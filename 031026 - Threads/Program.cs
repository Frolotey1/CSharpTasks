using System;
using System.Runtime.InteropServices;

namespace HomeTask;

public static class Program
{

    public static Task Main()
    {
        return Task.WhenAll(
            Task.Run(NotepadRunner.Run),
            Task.Run(TerminalRunner.Run));
    }
    public static bool GetNameOperatingSystem() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
}