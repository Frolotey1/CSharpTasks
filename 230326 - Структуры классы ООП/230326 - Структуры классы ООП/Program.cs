namespace _230326___Структуры_классы_ООП
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("1) Точка\n2) Пользователь\n3) Персональный компьютер\n4) Ноутбук\n5) Features книги и method книги\nВыберите опцию: ");
            int selectOption = int.Parse(Console.ReadLine());

            switch (selectOption)
            {
                case 1:
                    Point.Run();
                    break;
                case 2:
                    User.Run();
                    break;
                case 3:
                    PersonalComputer.Run();
                    break;
                case 4:
                    Laptop.Run();
                    break;
                case 5:
                    FeaturesBook.Run();
                    MethodBook.Run();
                    break;
                default:
                    Console.WriteLine("Нет такой опции");
                    break;
            }

        }
    }
}
