namespace Project;
using System;

public class Student : Human {
    private uint Course {get; set;}
    private uint Group {get; set;}
    public void Run() {
	Console.Write("Введите имя человека: ");
	Name = Console.ReadLine();
	Console.Write("Введите возраст человека: ");
	Age = uint.Parse(Console.ReadLine());
	Console.Write("Введите курс студента: ");
        Course = uint.Parse(Console.ReadLine());
	Console.Write("Введите группу студента: ");
	Group = uint.Parse(Console.ReadLine());

	Console.Write($"Имя и возраст: {info()}\nКурс: {Course}\nГруппа: {Group}");
    }
}
