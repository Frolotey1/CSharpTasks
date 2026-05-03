using System;
using Project;

public class Program {
    public static void Main() {
	Console.Write("1) Показ фильма\n2) Покупка + Оплата\n3) Подсчёт слов в тексте\n4) Выход\nВыберите опцию: ");

	int selectOption = int.Parse(Console.ReadLine());

	switch (selectOption) {
	    case 1:
		ShowFilm.Run();
		break;
	    case 2:
		Purchase<NonCash>.Run();
		break;
	    case 3:
		WordCounter.Run();
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
