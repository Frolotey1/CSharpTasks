using Project;
using System;

public class Program {
    public static void Main() {
	Console.Write("1) 1 задание\n2) 2 задание\n3) Выход из программы\nВыберите опцию: ");
	string select = Console.ReadLine();
	
	switch(int.Parse(select)) {
	    case 1:
		FirstTaskClass.FirstTask();
		break;
	    case 2:
		SecondTaskClass.SecondTask();
		break;
	    case 3:
		Console.WriteLine("Выход из программы");
		Environment.Exit(0);
		break;
	    default:
		Console.WriteLine("Такой опции нет");
		break;
	}	
    }
}
