using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Project;

public class Program {
    public static void Main() {
	Console.Write("1) Показ фильмая\n2) Покупка + Оплата + Безналичный + Наличный\n3) Количество слов в тексте\nВыберите опцию: ");
	int selectOption = int.Parse(Console.ReadLine());

	switch(selectOption) {
	    case 1:
		ShowFilm.Run();
		break;
	    case 2:
		Purchase<NonCash>.Run();
		break;
	    case 3:
		WordCounter.Run();
		break;
	    default:
		Console.WriteLine("Нет такой опции");
		break;
	}
    }
}
