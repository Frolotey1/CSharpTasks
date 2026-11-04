using ResearchLab;
using System.Diagnostics.Metrics;
using System.Diagnostics;

public static class Program
{
    private const int CellCount = 20;
    private const int ChargerSlots = 4;

    //private const int AutoCancelMs = 0;
    private const int AutoCancelMs = 2000;  
    //private const int AutoCancelMs = 3000;
    private const int ShutdownLimitMs = 10_000;
    private static Timer? _autoCancelTimer;

    public static int Main(string[] args)
    {
        using var cts = new CancellationTokenSource();
        var token = cts.Token;

        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            EventLog.Write("Включена аварийная остановка");
            try { 
                cts.Cancel(); 
            } catch (ObjectDisposedException exc) {
                Console.WriteLine($"Исключение: {exc.Message}");
            }
        };

        if(AutoCancelMs > 0)
        {
            _autoCancelTimer = new Timer(_ =>
            {
                EventLog.Write($"Включена аварийная остановка за {AutoCancelMs} мс");
                try
                {
                    cts.Cancel();
                }
                catch (ObjectDisposedException exc)
                {
                    Console.WriteLine($"Исключение: {exc.Message}");
                }
            }, null, AutoCancelMs, Timeout.Infinite);
        }

        ThreadPool.GetAvailableThreads(out int workerStart, out int ioStart);
        EventLog.Write($"Свободных рабочих потоков в пуле: {workerStart} | Свободных I/O потоков:{ioStart}");

        Random rnd = new Random();
        var cells = new List<Cell>(CellCount);
        for (int i = 1; i <= CellCount; i++)
            cells.Add(Cell.CreateRandom(i, rnd));

        string resultAutoCancel = !string.IsNullOrEmpty(AutoCancelMs.ToString())
                                    ? AutoCancelMs.ToString() : "выключена";

        EventLog.Write($"Количество партии ячеек: {cells.Count}\nСлотов зарядки={ChargerSlots}\nАвтоотмена={resultAutoCancel}");

        using var session = new StationSession(ChargerSlots, token);
        using var done = new CountdownEvent(cells.Count);

        var sw = Stopwatch.StartNew();

        foreach (var cell in cells)
        {
            var c = cell;
            EventLog.Write(c.Id, "ячейка поставлена в очередь", CellStatus.Pending);
            ThreadPool.QueueUserWorkItem(_ => CellProcessor.Process(c, session, done));
        }

        bool allDone = done.Wait(ShutdownLimitMs + 5_000);

        if (token.IsCancellationRequested)
        {
            if (!done.Wait(ShutdownLimitMs))
            {
                EventLog.Write("Превышен лимит ожидания завершения работы");
                sw.Stop();
                PrintStatistics(session.Counters, cells.Count, sw.Elapsed, workerStart, out _);
                return 2;
            }
        }
        else if (!allDone)
        {
            EventLog.Write("ОШИБКА: превышен лимит ожидания завершения работы (штатный режим)");
        }

        sw.Stop();
        EventLog.Write("обработка партии завершена");

        ThreadPool.GetAvailableThreads(out int workerEnd, out int ioEnd);
        EventLog.Write($"пул потоков после: доступно worker={workerEnd}, io={ioEnd}");

        Console.WriteLine("Итоговая статистика: ");
        PrintStatistics(session.Counters, cells.Count, sw.Elapsed, workerStart, out _);

        _autoCancelTimer?.Dispose();

        return 0;
    }

    private static void PrintStatistics(
        OwnCounter counters, int total, TimeSpan elapsed, int workerStart, out int workerEnd)
    {
        ThreadPool.GetAvailableThreads(out workerEnd, out _);

        int completed = counters.Completed;
        int canceled = counters.Canceled;
        int faulted = counters.Faulted;
        int sum = completed + canceled + faulted;

        EventLog.Write($"Всего ячеек:{total}");
        EventLog.Write($"Выполнено: {completed}");
        EventLog.Write($"Отменено: {canceled}");
        EventLog.Write($"Сброшено: {faulted}");
        EventLog.Write($"Сумма статусов: {sum}");
        EventLog.Write($"Общее время: {elapsed.TotalMilliseconds} мс");
        EventLog.Write($"Доступно свободных рабочих потоков в начале: {workerStart}");
        EventLog.Write($"Доступно свободных рабочих потоков в конце: {workerEnd}");
        EventLog.Write($"Успешных зарядок: {counters.SuccessfulCharges}");
        EventLog.Write($"Успешных калибровок: {counters.SuccessfulCalibrations}");
        EventLog.Write($"Прервано ожиданий заряда: {counters.CanceledChargerWaits}");
        EventLog.Write($"Прервано ожиданий калибр: {counters.CanceledCalibrationWaits}");

        if (sum != total)
            EventLog.Write($"Сумма статусов ({sum}) не равна числу ячеек ({total})!");
    }
}
