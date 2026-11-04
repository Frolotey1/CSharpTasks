namespace ResearchLab;
public class CellResult
{
    public int CellId { get; set; }
    public CellStatus FinalStatus { get; init; }
    public DateTime WhenStarted { get; init; }
    public DateTime WhenFinished { get; init; }
    public string FaultMessage { get; init; } = string.Empty;

    public override string ToString() =>
        $"Id: {CellId}" +
        $" | Итоговый статус: {FinalStatus}" +
        $" | Время начала обработки: {WhenStarted}" +
        $" | Время завершения обработки: {WhenFinished}" +
        $" | Сообщение об ошибке: {FaultMessage}";
}
