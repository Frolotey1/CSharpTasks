using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300326___Интерфейсы
{
    public class Manager : ISalary, IConsole
    {
        public string FullName { get; set; }
        public int WorkingDays { get; set; }

        public Manager(string fullName, int workingDays)
        {
            FullName = fullName;
            WorkingDays = workingDays;
        }
        public double CalculateSalary()
        {
            return WorkingDays * 1000;
        }
        public void Print()
        {
            Console.WriteLine($"Менеджер");
            Console.WriteLine($"ФИО: {FullName}");
            Console.WriteLine($"Количество рабочих дней: {WorkingDays}");
            Console.WriteLine($"Зарплата: {CalculateSalary()} руб.");
        }
    }
}
