using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _230326___Структуры_классы_ООП
{
    public class FeaturesBook
    {
        public string Title { get; set; }

        private string author;
        public string Author
        {
            get { return author; }
        }

        public string Publisher { get; private set; }

        private int year;
        private int pages;
        public FeaturesBook(string title, string author, string publisher, int year, int pages)
        {
            Title = title;
            this.author = author;
            Publisher = publisher;
            this.year = year;
            this.pages = pages;
        }

        public string Info
        {
            get
            {
                return $"Название: {Title}, Автор: {author}, Издательство: {Publisher}, Год выпуска: {year}, Объём: {pages} стр.";
            }
        }

        public static void Run()
        {
            Console.WriteLine("Книга");

            Console.Write("Введите название книги: ");
            string? title = Console.ReadLine();

            Console.Write("Введите автора: ");
            string? author = Console.ReadLine();

            Console.Write("Введите издательство: ");
            string? publisher = Console.ReadLine();

            Console.Write("Введите год выпуска: ");
            int year = int.Parse(Console.ReadLine());

            Console.Write("Введите объём в листах (страницах): ");
            int pages = int.Parse(Console.ReadLine());

            FeaturesBook book = new FeaturesBook(title, author, publisher, year, pages);

            Console.WriteLine("Информация о книге");
            Console.WriteLine(book.Info);

            Console.WriteLine("Проверка работы свойств");

            Console.Write("Введите новое название книги: ");
            book.Title = Console.ReadLine();
            Console.WriteLine($"Новое название: {book.Title}");

            Console.WriteLine($"Автор (только чтение): {book.Author}");

            Console.WriteLine($"Издательство (только чтение): {book.Publisher}");
        }
    }
}
