using System;

namespace _230326___Структуры_классы_ООП
{
    public class Point
    {
        private double X { get; set; }
        private double Y { get; set; }
        private double Z { get; set; }

        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void MoveBy(double dx, double dy, double dz)
        {
            X += dx;
            Y += dy;
            Z += dz;
        }

        public void Show()
        {
            Console.WriteLine($"Точка: ({X}, {Y}, {Z})");
        }

        public static void Run()
        {

            Console.Write("Введите X: ");
            double x = double.Parse(Console.ReadLine());

            Console.Write("Введите Y: ");
            double y = double.Parse(Console.ReadLine());

            Console.Write("Введите Z: ");
            double z = double.Parse(Console.ReadLine());

            Point p = new Point(x, y, z);
            Console.Write("Исходная точка: ");
            p.Show();

            Console.Write("Введите dx (смещение по X): ");
            double dx = double.Parse(Console.ReadLine());

            Console.Write("Введите dy (смещение по Y): ");
            double dy = double.Parse(Console.ReadLine());

            Console.Write("Введите dz (смещение по Z): ");
            double dz = double.Parse(Console.ReadLine());

            p.MoveBy(dx, dy, dz);
            Console.Write("Точка после перемещения: ");
            p.Show();
        }
    }
}
