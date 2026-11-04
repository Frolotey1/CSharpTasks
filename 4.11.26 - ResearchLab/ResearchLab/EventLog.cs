using ResearchLab;
public class EventLog
{
    public static readonly object Sync = new();

    public static void Write(int cellId, string evnt, CellStatus? status = null)
    {
        int currentThreadId = Environment.CurrentManagedThreadId;
        string ts = DateTime.Now.ToString("HH:mm:ss");
        string describeStatus = status != null ? $"{status}" : "";

        lock (Sync)
        {
            Console.WriteLine($"Идентификатор ячейки: {cellId}");
            Console.WriteLine($"Идентификатор потока: {currentThreadId}");
            Console.WriteLine($"Временная метка: {ts}");
            Console.WriteLine($"Описание события: {evnt}");
            Console.WriteLine($"Текущий статус ячейки: {describeStatus}");
        }
    }
    public static void Write(string evnt)
    {
        int currentThreadId = Environment.CurrentManagedThreadId;
        string ts = DateTime.Now.ToString("HH:mm:ss.fff");

        lock(Sync)
        {
            Console.WriteLine($"Идентификатор потока: {currentThreadId}");
            Console.WriteLine($"Описание события: {evnt}");
        }
    }
}