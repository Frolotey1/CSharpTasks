using System;
using Patterns;

public class Program {
    public static void Main() {
	Console.Write("1) Factory Method (создание виджетов)\n2) Abstract Factory (создание UI-компонентов)\n3) Связка Factory Method + Abstract Factory\n4) Выход\nВыберите опцию: ");
            
	int selectOption = int.Parse(Console.ReadLine());
            
	switch (selectOption) {
	    case 1:
		FactoryMethodDemo.Run();
		break;
	    case 2:
		AbstractFactoryDemo.Run();
		break;
	    case 3:
		IntegrationDemo.Run();
		break;
	    case 4:
		Console.WriteLine("Завершение программы");
		return;
	    default:
		Console.WriteLine("Нет такой опции");
		break;
	}	
    }
}
