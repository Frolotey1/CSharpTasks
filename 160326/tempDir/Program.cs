namespace C_ {
	using System.Threading;
	using System;
	using System.Text;
	public class Program {
		public static void Main() {
			int[] tasks = new int[4];

			for(int i = 0; i < 4; ++i) {
				tasks[i] = i + 1;
			}

			foreach(var task in tasks) {
				Console.WriteLine($"Задание {task}");
			}
			Console.WriteLine("Выход");
	
			string selectTask = "";
			Console.WriteLine("Выберите задание: ");
			selectTask = Console.ReadLine();

			if(selectTask == "Выход") {
				Console.WriteLine("Завершение программы");
				Environment.Exit(0);
			}
			
			switch(int.Parse(selectTask)) {
				case 1:
					FirstTask.Run();
				break;
				case 2:
					SecondTask.Run();
				break;
				case 3:
				        ThirdTask.Run();
				break;
				case 4:
					FourthTask.Run();
				break;
				default:
					Console.WriteLine("Такого задания нет");
				break;
			}
		}
	}
	
	public class FirstTask {
		public static void Run() {
			Console.WriteLine("Нажмите на клавишу для запуска первого задания: ");
			Console.ReadKey();
						
			int A = 0, B = 0, C = 0;

			while(true) {
				
				Console.Write("Введите число A: ");
				A = int.Parse(Console.ReadLine());
				Console.Write("Введите число B: ");
				B = int.Parse(Console.ReadLine());
				Console.Write("Введите число С: ");
				C = int.Parse(Console.ReadLine());
				
				if(A < 0 || B < 0 || C < 0) {
					Console.Write("Введены ошибочные данные.");
				} else if(C > A && C > B) {
					Console.Write("Сторона квадрата больше ширины и высоты прямоугольника");
				} else {
					int resultWidth = A / C, resultHeight = B / C;
					int countSquare = resultWidth * resultHeight;
					Console.Write($"Всего квадратов: {countSquare}");
					
					int rectangleArea = A * B;
					int squareArea = countSquare * (C * C);
					Console.WriteLine($"Площадь незанятого участка прямоугольника: {rectangleArea - squareArea}");
					
					Thread.Sleep(3000);
					Console.Clear();
					break;
				}
			}
			
			int backToList = 0;
			Console.Write("Для возвращения к списку задач напишите 1: ");
			backToList = int.Parse(Console.ReadLine());

			if(backToList == 1) {
				Program.Main();
			} else {
				Console.WriteLine("Завершение программы");
				Environment.Exit(0);
			}
		}
	}
	
	public class SecondTask {
		public static void Run() {
			Console.WriteLine("Нажмите на клавишу для запуска второго задания: ");
			Console.ReadKey();

			double salary = 10000.0, P = 0.0;
			
			while(true) {
				Console.Write("Напишите процент P: ");
				P = double.Parse(Console.ReadLine());
				
				if(P > 0.0 && P < 25.0) {
					break;
				}
			}

			int month = 0;

			while(salary < 11000.0) {
				salary += salary * P / 100;
				month++;
			}
			
			Console.WriteLine($"Сумма {salary} была набрана за {month} месяц(-а)");
			Thread.Sleep(3000);
			Console.Clear();

			int backToList = 0;
			Console.Write("Для возврашения в список с заданиями напишите 1: ");
			backToList = int.Parse(Console.ReadLine());

			if(backToList == 1) {
				Program.Main();
			} else {
				Console.WriteLine("Завершение программы");
				Environment.Exit(0);
			}			
		}

	}

	public class ThirdTask {
		public static void Run() {
			Console.WriteLine("Нажмите на клавишу для запуска третьего задания: ");
			Console.ReadKey();

			int A = 0, B = 0;

			while(true) {
				Console.Write("Напишите число A: ");
				A = int.Parse(Console.ReadLine());
				Console.Write("Напишите число B: ");
				B = int.Parse(Console.ReadLine());

				if(A > B) {
					Console.WriteLine($"Число {A} больше {B}.");
				} else {
					B++;
					while(A < B) {
						int generateA = 0;
						while(generateA < A) {
							Console.Write($"{A} ");
							generateA++;
						}
						Console.Write('\n');
						A++;
					}

					break;
				}	
			}		
			
			int backToList = 0;
			Console.Write("Для возврашения к списку задач напишите 1: ");
			backToList = int.Parse(Console.ReadLine());

			if(backToList == 1) {
				Program.Main();
			} else {
				Console.WriteLine("Завершение программы");
				Environment.Exit(0);
			}
		}
	}

	public class FourthTask {
		public static void Run() {
			Console.WriteLine("Нажмите на клавишу для запуска четвертого задания");
			Console.ReadKey();

			int N = 0;
			string toString = "";
			
			while(true) {
				Console.Write("Напишите число N: ");
				N = int.Parse(Console.ReadLine());

				if(N < 0) {
					Console.WriteLine("Число должно быть положительным");
				} else {
					toString = N.ToString();
					break;
				}
				
			}

			if(toString.Length > 1) {
				var changeSymbolsInString = new StringBuilder(toString);
				char temp = changeSymbolsInString[0];
				changeSymbolsInString[0] = changeSymbolsInString[^1];
				changeSymbolsInString[^1] = temp;
				toString = changeSymbolsInString.ToString();
			}

			Console.WriteLine(toString);

			int backToList = 0;
			Console.Write("Для возвращения к списку заданий напишите 1: ");
			backToList = int.Parse(Console.ReadLine());

			if(backToList == 1) {
				Program.Main();
			} else {
				Console.WriteLine("Завершение программы");
				Environment.Exit(0);	
			}
		}
	}
}

