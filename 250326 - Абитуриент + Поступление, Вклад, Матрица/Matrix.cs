namespace Project;
using System;

public class Matrix
{
    private int[,] data;
    private int rows;
    private int cols;
    public Matrix(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
        data = new int[rows, cols];
    }
    public int this[int i, int j]
    {
        get { return data[i, j]; }
        set { data[i, j] = value; }
    }
    public void FillManually(string name = "Матрица")
    {
        Console.WriteLine($"Заполните {name} {rows}x{cols}:");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"{name}[{i},{j}] = ");
                data[i, j] = int.Parse(Console.ReadLine());
            }
        }
    }
    public void Print(string name = "Матрица")
    {
        Console.WriteLine($"\n{name}:");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"{data[i, j],5} ");
            }
            Console.WriteLine();
        }
    }
    public static Matrix operator +(Matrix a, Matrix b)
    {
        if (a.rows != b.rows || a.cols != b.cols)
        {
            throw new ArgumentException("Матрицы должны быть одинакового размера!");
        }

        Matrix result = new Matrix(a.rows, a.cols);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.cols; j++)
            {
                result[i, j] = a[i, j] + b[i, j];
            }
        }
        return result;
    }
    public static Matrix operator -(Matrix a, Matrix b)
    {
        if (a.rows != b.rows || a.cols != b.cols)
        {
            throw new ArgumentException("Матрицы должны быть одинакового размера!");
        }

        Matrix result = new Matrix(a.rows, a.cols);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.cols; j++)
            {
                result[i, j] = a[i, j] - b[i, j];
            }
        }
        return result;
    }
    public static Matrix operator *(Matrix m, int number)
    {
        Matrix result = new Matrix(m.rows, m.cols);
        for (int i = 0; i < m.rows; i++)
        {
            for (int j = 0; j < m.cols; j++)
            {
                result[i, j] = m[i, j] * number;
            }
        }
        return result;
    }
    public static Matrix operator *(int number, Matrix m)
    {
        return m * number;
    }
    public static Matrix operator *(Matrix a, Matrix b)
    {
        if (a.cols != b.rows)
        {
            throw new ArgumentException("Количество столбцов первой матрицы должно равняться количеству строк второй!");
        }

        Matrix result = new Matrix(a.rows, b.cols);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < b.cols; j++)
            {
                int sum = 0;
                for (int k = 0; k < a.cols; k++)
                {
                    sum += a[i, k] * b[k, j];
                }
                result[i, j] = sum;
            }
        }
        return result;
    }
    public static Matrix operator +(Matrix m, int number)
    {
        Matrix result = new Matrix(m.rows, m.cols);
        for (int i = 0; i < m.rows; i++)
        {
            for (int j = 0; j < m.cols; j++)
            {
                result[i, j] = m[i, j] + number;
            }
        }
        return result;
    }
    public static Matrix operator +(int number, Matrix m)
    {
        return m + number;
    }
    public static Matrix operator -(Matrix m, int number)
    {
        Matrix result = new Matrix(m.rows, m.cols);
        for (int i = 0; i < m.rows; i++)
        {
            for (int j = 0; j < m.cols; j++)
            {
                result[i, j] = m[i, j] - number;
            }
        }
        return result;
    }
    public static bool operator >(Matrix a, Matrix b)
    {
        return a.GetSum() > b.GetSum();
    }

    public static bool operator <(Matrix a, Matrix b)
    {
        return a.GetSum() < b.GetSum();
    }
    private int GetSum()
    {
        int sum = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                sum += data[i, j];
            }
        }
        return sum;
    }

    public static void Run()
    {

        Console.Write("Введите количество строк: ");
        int rows = int.Parse(Console.ReadLine());
        Console.Write("Введите количество столбцов: ");
        int cols = int.Parse(Console.ReadLine());

        Matrix m1 = new Matrix(rows, cols);
        Matrix m2 = new Matrix(rows, cols);

        m1.FillManually("Первая матрица");
        m1.Print("Первая матрица");

        m2.FillManually("Вторая матрица");
        m2.Print("Вторая матрица");

        Matrix sum = m1 + m2;
        sum.Print("Сумма матриц (m1 + m2)");

        Matrix diff = m1 - m2;
        diff.Print("Разность матриц (m1 - m2)");

        Console.Write("\nВведите число для умножения: ");
        int num = int.Parse(Console.ReadLine());
        Matrix mulNum = m1 * num;
        mulNum.Print($"Умножение первой матрицы на {num}");

        Matrix addNum = m1 + num;
        addNum.Print($"Сложение первой матрицы с числом {num}");

        Matrix subNum = m1 - num;
        subNum.Print($"Вычитание числа {num} из первой матрицы");

        Console.WriteLine($"Сравнение матриц (по сумме элементов):");
        if (m1 > m2)
            Console.WriteLine("Первая матрица больше второй");
        else if (m1 < m2)
            Console.WriteLine("Первая матрица меньше второй");
        else
            Console.WriteLine("Матрицы равны по сумме элементов");

        Console.Write("\nВведите количество строк для третьей матрицы (для умножения): ");
        int rows3 = int.Parse(Console.ReadLine());
        Console.Write("Введите количество столбцов для третьей матрицы: ");
        int cols3 = int.Parse(Console.ReadLine());

        Matrix m3 = new Matrix(rows3, cols3);
        m3.FillManually("Третья матрица");
        m3.Print("Третья матрица");

        try
        {
            Matrix mulMat = m1 * m3;
            mulMat.Print("Результат умножения первой и третьей матриц");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
