using System;
using System.Collections.Generic;
using Shop;
using Project;
using SevenWonders;

public class Program {	
    public static void Main() {
        Request request = new Request();
        WondersDemo sevenWonders = new WondersDemo(); 
        Population population = new Population();
        Student student = new Student();
        
        Console.WriteLine("Нажмите на клавишу для запуска программы: ");
        Console.ReadKey();

        Console.Write("1) Товар и позиции\n2) Студент\n3) 7 чудес света\n4) Население трех стран\n5) Выход\nВыберите пункт для демонстрации работы ООП:\n");
        int select = int.Parse(Console.ReadLine());

        switch(select) {
            case 1:
                request.Run();
                break;
            case 2:
                student.Run();
                break;
            case 3:
                sevenWonders.Run();
                break;
            case 4:
                population.Run();
                break;
            case 5:
                Console.WriteLine("Завершение программы");
                break;
            default:
                Console.WriteLine("Такого пункта нет");
                break;		    
        }
    }
}
