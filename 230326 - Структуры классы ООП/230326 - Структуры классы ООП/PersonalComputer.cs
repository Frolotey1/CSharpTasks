using System;
using System.Text;

namespace _230326___Структуры_классы_ООП
{
    public class PersonalComputer
{
    private string Model;
    private int CpuFrequency;
    private int RamSize;
    private int HddSize;

    public PersonalComputer(string model, int cpuFrequency, int ramSize, int hddSize)
    {
        Model = model;
        CpuFrequency = cpuFrequency;
        RamSize = ramSize;
        HddSize = hddSize;
    }

    public string GetInfo()
    {
        return $"Модель: {Model}, Процессор: {CpuFrequency} ГГц, ОЗУ: {RamSize} ГБ, Жёсткий диск: {HddSize} ГБ";
    }

    public static void Run() 
    {
        
        Console.Write("Введите модель ПК: ");
        string? model = Console.ReadLine();
        
        Console.Write("Введите тактовую частоту процессора (ГГц): ");
        int cpuFreq = int.Parse(Console.ReadLine());
        
        Console.Write("Введите объём оперативной памяти (ГБ): ");
        int ram = int.Parse(Console.ReadLine());
        
        Console.Write("Введите объём жёсткого диска (ГБ): ");
        int hdd = int.Parse(Console.ReadLine());
        
        PersonalComputer pc = new PersonalComputer(model, cpuFreq, ram, hdd);
        Console.WriteLine(pc.GetInfo());
    }
}
}
