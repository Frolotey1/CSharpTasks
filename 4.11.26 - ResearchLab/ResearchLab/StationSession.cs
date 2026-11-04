using ResearchLab;
using System.Collections.Concurrent;

public class StationSession : IDisposable
{
    public SemaphoreSlim ChargingCellsLimitation { get; }
    public Mutex SingleCalibration { get; }
    public CancellationToken AwaitCancellationToken { get; }
    public ConcurrentDictionary<int, CellResult> Results { get; set; } = new();
    public OwnCounter Counters { get; } = new();

    public StationSession(int chargerSlots, CancellationToken _token)
    {
        ChargingCellsLimitation = new SemaphoreSlim(chargerSlots, chargerSlots);
        SingleCalibration = new Mutex(initiallyOwned: false);
        AwaitCancellationToken = _token;
    }

    public void Dispose()
    {
        ChargingCellsLimitation.Dispose();
        SingleCalibration.Dispose();
    }
}