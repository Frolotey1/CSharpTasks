namespace Project;

using System;

public static class SecondTaskClass {
    public static void SecondTask() {
	Console.WriteLine("Нажмите на клавишу для запуска второго задания: ");
	Console.ReadKey();

	int N = 0, num = 0, i = 0;

	while(true) {
	    Console.Write("Напишите размер для массива: ");
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

		Console.WriteLine("Изначальный вид массива: ");
		foreach(var elem in array) {
		    Console.Write($"{elem} ");
		}
		Console.WriteLine("");

		for(int j = 0; j < N - 1; ++j) {
		    if(array[j] > array[j + 1]) {
			int temp = array[j];
			array[j] = array[j + 1];
			array[j + 1] = temp;
		    }
		}

		Console.WriteLine("Отсортированный массив: ");

		foreach(var result in array) {
		    Console.Write($"{result} ");
		}
		Console.WriteLine("");

		break;
	    }
	}
    }
}
