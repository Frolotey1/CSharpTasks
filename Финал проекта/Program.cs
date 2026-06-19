using Patterns;
using Patterns.Capstone;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("CrossPlatformUISimulator\n");

	    FullLifecycleCapstoneRunner flcr = new FullLifecycleCapstoneRunner();
	    flcr.Run();
        
    }
}
