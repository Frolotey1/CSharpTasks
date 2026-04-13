using System;

namespace _230326___Структуры_классы_ООП
{
    public class MethodBook
    {
        private string title;
        private string author;
        private string publisher;
        private int year;
        private int pages;
        public MethodBook(string title, string author, string publisher, int year, int pages)
        {
            this.title = title;
            this.author = author;
            this.publisher = publisher;
            this.year = year;
            this.pages = pages;
        }

        public void SetTitle(string newTitle)
        {
            title = newTitle;
        }

        public string GetTitle()
        {
            return title;
        }

        public string GetAuthor()
        {
            return author;
        }

        public string GetPublisher()
        {
            return publisher;
        }

        public int GetYear()
        {
            return year;
        }
        public int GetPages()
        {
            return pages;
        }

        public string GetInfo()
        {
            return $"Название: {title}, Автор: {author}, Издательство: {publisher}, Год выпуска: {year}, Объём: {pages} стр.";
        }

        public static void Run()
        {
            Console.WriteLine("Книга");

            Console.Write("Введите название книги: ");
            string title = Console.ReadLine();

            Console.Write("Введите автора: ");
            string author = Console.ReadLine();

            Console.Write("Введите издательство: ");
            string publisher = Console.ReadLine();

            Console.Write("Введите год выпуска: ");
            int year = int.Parse(Console.ReadLine());

            Console.Write("Введите объём в листах (страницах): ");
            int pages = int.Parse(Console.ReadLine());

            MethodBook book = new MethodBook(title, author, publisher, year, pages);

            Console.WriteLine("Информация о книге");
            Console.WriteLine(book.GetInfo());

            Console.Write("Введите новое название книги: ");
            string? newTitle = Console.ReadLine();
            book.SetTitle(newTitle);
            Console.WriteLine($"Новое название: {book.GetTitle()}");
            Console.WriteLine($"Автор (только чтение): {book.GetAuthor()}");
            Console.WriteLine($"Издательство (только чтение): {book.GetPublisher()}");
        }
    }
}
