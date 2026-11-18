using ResearchLab;

public static class CellProcessor
{
    private static long _sink;
    public static void Process(Cell cell, StationSession stationSession, CountdownEvent done)
    {
        OwnCounter counters = stationSession.Counters;
        CancellationToken token = stationSession.AwaitCancellationToken;

        CellStatus status = CellStatus.Pending;
        DateTime startedAt = DateTime.Now;
        string faultMessage = "";

        try
        {

            token.ThrowIfCancellationRequested();

            status = CellStatus.Diagnostics;
            EventLog.Write(cell.Id,"Диагностика начата",status);

            long accumulator = 0;
            for (int i = 0; i < cell.DiagnosticIterations; ++i)
            {
                if (i % 1024 == 0)
                    token.ThrowIfCancellationRequested();

                unchecked
                {
                    accumulator = accumulator * 31 + i;
                }
            }

            Volatile.Write(ref _sink, accumulator);

            EventLog.Write(cell.Id,"Диагностика завершена",status);

            status = CellStatus.WaitingForCharger;
            EventLog.Write(cell.Id, "Ожидание слота зарядки", status);

            bool charger = false;
            try
            {
                try
                {
                    stationSession.ChargingCellsLimitation.Wait(token);
                } catch (OperationCanceledException)
                {
                    counters.MarkCanceledChargerWaits();
                    throw;
                }
                charger = true;

                token.ThrowIfCancellationRequested();

                status = CellStatus.Charging;

                EventLog.Write(cell.Id,"Слот зарядки получен и зарядка началась",status);

                if (cell.ChargeDuration > TimeSpan.Zero &&
                   token.WaitHandle.WaitOne(cell.ChargeDuration))
                    token.ThrowIfCancellationRequested();

                EventLog.Write(cell.Id,"Зарядка завершена",status);
                counters.MarkSuccessfulCharges();
            }
            finally
            {
                if (charger)
                {
                    stationSession.ChargingCellsLimitation.Release();
                    EventLog.Write(cell.Id,"Слот зарядки освобожден",status: null);
                }
            }

            bool mutex = false;
            try
            {
                status = CellStatus.WaitingForCalibration;
                EventLog.Write(cell.Id,"Ожидание калибровочного стенда",status);

                int idx = WaitHandle.WaitAny([stationSession.SingleCalibration, token.WaitHandle]);

                if (idx == 1)
                {
                    counters.MarkCanceledCalibrationWaits();
                    token.ThrowIfCancellationRequested();
                }

                status = CellStatus.Calibration;
                EventLog.Write(cell.Id,"Калибровочный стенд получен",status);
                EventLog.Write(cell.Id,"Калибровка началась",status);

                mutex = true;
                token.ThrowIfCancellationRequested();

                if (cell.CalibrationDuration > TimeSpan.Zero &&
                    token.WaitHandle.WaitOne(cell.CalibrationDuration))
                    token.ThrowIfCancellationRequested();

                EventLog.Write(cell.Id, "Калибровка завершена", status);
                counters.MarkSuccessfulCalibrations();
            }
            finally
            {
                if (mutex)
                {
                    stationSession.SingleCalibration.ReleaseMutex();
                    EventLog.Write(cell.Id,"Калибровочный стенд освобожден",status);
                }
            }

            counters.MarkCompleted();
        }
        catch (OperationCanceledException exc)
        {
            counters.MarkCanceled();
            status = CellStatus.Canceled;
            faultMessage = exc.Message;
        }
        catch (Exception exc)
        {
            counters.MarkFaulted();
            status = CellStatus.Faulted;
            faultMessage = exc.Message;
        }
        finally
        {
            CellResult result = new CellResult
            {
                CellId = cell.Id,
                FinalStatus = status,
                WhenStarted = startedAt,
                WhenFinished = DateTime.Now,
                FaultMessage = faultMessage
            };

            stationSession.Results[cell.Id] = result;
            done.Signal();
        }
    }
}