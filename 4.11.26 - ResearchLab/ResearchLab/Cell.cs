namespace ResearchLab;
public class Cell
{
    public int Id { get; init; }
    public int DiagnosticIterations { get; init; }
    public TimeSpan ChargeDuration { get; init; }
    public TimeSpan CalibrationDuration { get; init; }


    public Cell(int id, int diagnosticIterations, TimeSpan chargeDuration, TimeSpan calibrationDuration)
    {
        Id = id;
        DiagnosticIterations = diagnosticIterations;
        ChargeDuration = chargeDuration;
        CalibrationDuration = calibrationDuration;
    }

    public static Cell CreateRandom(int id, Random random)
    {
        Cell cell = new Cell(
            id,
            100_000,
            TimeSpan.FromMilliseconds(random.Next(300, 901)),
            TimeSpan.FromMilliseconds(random.Next(100, 301)));

        return cell;
    }
}