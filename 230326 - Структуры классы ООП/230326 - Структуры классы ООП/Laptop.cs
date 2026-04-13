using System;

namespace _230326___Структуры_классы_ООП
{
    public class Laptop
    {
        private string Model;
        private double CpuFrequency;
        private int RamSize;
        private int HddSize;
        private double Weight;

        public Laptop(string model, double cpuFrequency, int ramSize, int hddSize, double weight)
        {
            Model = model;
            CpuFrequency = cpuFrequency;
            RamSize = ramSize;
            HddSize = hddSize;
            Weight = weight;
        }

        public string GetInfo()
        {
            return $"Ноутбук: {Model}, Процессор: {CpuFrequency} ГГц, ОЗУ: {RamSize} ГБ, Жёсткий диск: {HddSize} ГБ, Масса: {Weight} кг";
        }

        public static void Run()
        {
            Console.Write("Введите модель ноутбука: ");
            string? model = Console.ReadLine();

            Console.Write("Введите тактовую частоту процессора (ГГц): ");
            double cpuFreq = double.Parse(Console.ReadLine());

            Console.Write("Введите объём оперативной памяти (ГБ): ");
            int ram = int.Parse(Console.ReadLine());

            Console.Write("Введите объём жёсткого диска (ГБ): ");
            int hdd = int.Parse(Console.ReadLine());

            Console.Write("Введите массу ноутбука (кг): ");
            double weight = double.Parse(Console.ReadLine());

            Laptop laptop = new Laptop(model, cpuFreq, ram, hdd, weight);
            Console.WriteLine(laptop.GetInfo());
        }
    }
}
