using System;
using System.Globalization;
using Project;

public class Program {
    public static void Main() {
	Console.WriteLine("1) Абитуриент + Поступление\n2) Вклад\n3) Кредит\n4) Матрица\n5) Выход\nВыберите опцию: ");
	int selectOption = int.Parse(Console.ReadLine());

	switch(selectOption) {
	    case 1:
		Applicant.Run();
		break;
	    case 2:
	        Deposit.Run();
		break;
	    case 3:
	        Credit.Run();
		break;
	    case 4:
		Matrix.Run();
		break;
	    case 5:
		Console.WriteLine("Завершение программы");
		break;
	    default:
		Console.WriteLine("Нет такой опции");
		break;
	}
    }
}
