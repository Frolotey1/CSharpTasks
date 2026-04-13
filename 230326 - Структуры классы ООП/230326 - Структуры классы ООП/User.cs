using System;


namespace _230326___Структуры_классы_ООП
{
    public class User
    {
        private string Surname { get; set; }
        private string Name { get; set; }
        private string Lastname { get; set; }
        private int Age { get; set; }

        public User(string surname, string name, string lastname, int age)
        {
            Surname = surname;
            Name = name;
            Lastname = lastname;
            Age = age;
        }

        public string GetFullName()
        {
            return $"{Surname} {Name} {Lastname}";
        }

        public void Info()
        {
            Console.WriteLine($"ФИО: {GetFullName()}");
            Console.WriteLine($"Возраст: {Age}");
        }

        public static void Run()
        {

            Console.Write("Введите фамилию: ");
            string? surname = Console.ReadLine();

            Console.Write("Введите имя: ");
            string? name = Console.ReadLine();

            Console.Write("Введите отчество: ");
            string? patronymic = Console.ReadLine();

            Console.Write("Введите возраст: ");
            int age = int.Parse(Console.ReadLine());

            User user = new User(surname, name, patronymic, age);
            user.Info();
        }
    }
}
