using System;
using Project;

public class Program {
    public static void Main() {
        Console.WriteLine("Главное меню");
            Console.WriteLine("1) Конвертер температур");
            Console.WriteLine("2) Зарплата");
            Console.WriteLine("3) Клиент антикафе");
            Console.WriteLine("4) Выход");
            Console.Write("Выберите опцию: ");

            int selectOption = int.Parse(Console.ReadLine());

            switch (selectOption) {
                case 1:
                    Converter.Run();
                    break;
                case 2:
                    Salary.Run();
                    break;
                case 3:
                    AntiCoffee.Run();
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
