using System.Diagnostics;

namespace HomeTask;

public static class NotepadRunner {
    public static void Run()
    {
        Console.WriteLine("[Поток 1] Запуск потока");

        Process editor = new();
        editor.StartInfo.FileName = Program.GetNameOperatingSystem() ? "notepad.exe" : "kate";
        editor.StartInfo.UseShellExecute = true;

        Console.WriteLine($"[Поток 1] Запуск текстового редактора: {editor.StartInfo.FileName}");

        try
        {
            editor.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Не удалось запустить текстовый редактор '{editor.StartInfo.FileName}': {ex.Message}");
            return;
        }

        Console.WriteLine("[Поток 1] Ожидание работы редактора за 2 секунды");
        Thread.Sleep(2000);

        try
        {
            if (!editor.HasExited)
            {
                Console.WriteLine($"[Поток 1] Закрытие текстового редактора {editor.StartInfo.FileName}");
                editor.Kill(entireProcessTree: true);
            }
            else
            {
                Console.WriteLine($"Текстовый редактор: {editor.StartInfo.FileName}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Поток 1] Ошибка закрытия редактора '{editor.StartInfo.FileName}': {ex.Message}");
        }

        Console.WriteLine("[Поток 1] Поток 1 завершен");

    }
}
