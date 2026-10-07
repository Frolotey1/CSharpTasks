using System.Diagnostics;

namespace HomeTask;

public static class TerminalRunner
{
    public static void Run()
    {
        Console.WriteLine("[Поток 2] Запуск потока.");

        string shell = Program.GetNameOperatingSystem() ? "cmd.exe" : "/usr/bin/bash";
        string cmd = Program.GetNameOperatingSystem() ? "dir" : "ls -la";

        Process cmdProcess = new();
        cmdProcess.StartInfo.FileName = shell;
        cmdProcess.StartInfo.RedirectStandardInput = true;
        cmdProcess.StartInfo.RedirectStandardOutput = true;
        cmdProcess.StartInfo.RedirectStandardError = true;
        cmdProcess.StartInfo.UseShellExecute = false;
        cmdProcess.StartInfo.CreateNoWindow = true;

        Console.WriteLine($"[Поток 2] Запуск оболочки: {shell}");
        cmdProcess.Start();

        Console.WriteLine($"[Поток 2] Отправка команды '{cmd}'.");
        cmdProcess.StandardInput.WriteLine(cmd);
        cmdProcess.StandardInput.Close();

        string output = cmdProcess.StandardOutput.ReadToEnd();
        string errors = cmdProcess.StandardError.ReadToEnd();
        cmdProcess.WaitForExit();

        Console.WriteLine($"[Поток 2] Результат команды '{cmd}':");
        Console.WriteLine(output);

        if (!string.IsNullOrWhiteSpace(errors))
            Console.WriteLine("[Поток 2] stderr:\n" + errors);

        Console.WriteLine("[Поток 2] Поток завершён.");
    }
}
