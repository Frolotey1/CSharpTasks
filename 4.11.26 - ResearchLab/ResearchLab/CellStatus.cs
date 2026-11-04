namespace ResearchLab;
public enum CellStatus
{
    Pending,
    Diagnostics,
    WaitingForCharger,
    Charging,
    WaitingForCalibration,
    Calibration,
    Completed,
    Canceled,
    Faulted
}