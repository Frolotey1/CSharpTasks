namespace Project;

using System;

public static class ThirdTaskClass {
    public static void ThirdTask() {
	Console.WriteLine("Нажмите на клавишу для запуска третьего задания: ");
	Console.ReadKey();

	int N = 0, num = 0, i = 0;

	while(true) {
	    Console.Write("Напишите размер массива: ");
	    N = int.Parse(Console.ReadLine());

	    if(N < 0) {
		Console.WriteLine("Массив не может быть отрицательным");
	    } else {
		int[] array = new int[N];
		
		while(i < N) {
		    Console.Write("Напишите число: ");
		    num = int.Parse(Console.ReadLine());
		    array[i] = num;
		    i++;
		}

		Console.WriteLine("Массив: ");
		foreach(var elem in array) {
		    Console.Write($"{elem} ");
		}
		Console.WriteLine("");

		Console.Write("Напишите число: ");
		int findNumber = int.Parse(Console.ReadLine());
		int count = 0;

		foreach(var find in array) {
		    if(find == findNumber) {
			count++;
		    }  
		}

		Console.WriteLine($"Число '{findNumber}' встречается в массиве {count} раза");
		
		break;
	    }
	}
    }
}
