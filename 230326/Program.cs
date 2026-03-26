using Project;
using System;
public class Program {
    public static void Main() {
	Console.WriteLine("Нажмите на клавишу для запуска программы: ");
	Console.ReadKey();

	for(int i = 0; i < 4; ++i) {
	    Console.WriteLine($"{i + 1}) {i + 1} задание");
	}
        Console.WriteLine("5) выход");

	Console.Write("Выберите опцию: ");
	string select = Console.ReadLine();

	
	switch(int.Parse(select)) {
	    case 1:
		FirstTaskClass.FirstTask();
		break;
	    case 2:
		SecondTaskClass.SecondTask();
		break;
	    case 3:
		ThirdTaskClass.ThirdTask();
		break;
	    case 4:
		FourthTaskClass.FourthTask();
		break;
	    case 5:
		Console.WriteLine("Завершение программы");
		Environment.Exit(0);
		break;
	    default:
		Console.WriteLine("Нет такой опции");
		break;
	}
    }
}
