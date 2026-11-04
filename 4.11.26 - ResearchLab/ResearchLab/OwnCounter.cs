namespace ResearchLab;
public sealed class OwnCounter
{
    private int _completed;
    private int _faulted;
    private int _canceled;
    private int _successfulCharges;
    public int _successfulCalibrations;
    private int _canceledChargerWaits;
    private int _canceledCalibrationWaits;

    public int Completed => Volatile.Read(ref _completed);
    public int Faulted => Volatile.Read(ref _faulted);
    public int Canceled => Volatile.Read(ref _canceled);
    public int SuccessfulCharges => Volatile.Read(ref _successfulCharges);
    public int SuccessfulCalibrations => Volatile.Read(ref _successfulCalibrations);
    public int CanceledChargerWaits => Volatile.Read(ref _canceledChargerWaits);
    public int CanceledCalibrationWaits => Volatile.Read(ref _canceledCalibrationWaits);


    public void MarkCompleted() => Interlocked.Increment(ref _completed);
    public void MarkCanceled() => Interlocked.Increment(ref _canceled);
    public void MarkFaulted() => Interlocked.Increment(ref _faulted);
    public void MarkSuccessfulCharges() => Interlocked.Increment(ref _successfulCharges);
    public void MarkSuccessfulCalibrations() => Interlocked.Increment(ref _successfulCalibrations);
    public void MarkCanceledChargerWaits() => Interlocked.Increment(ref _canceledChargerWaits);
    public void MarkCanceledCalibrationWaits() => Interlocked.Increment(ref _canceledCalibrationWaits);

}