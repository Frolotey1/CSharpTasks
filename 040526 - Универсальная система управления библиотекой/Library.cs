namespace Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

public class Library<T> : IMediaManager<T> where T : Media
{
    private List<T> mediaList;
    private Dictionary<string, T> mediaDictionary;

    public Library()
    {
        mediaList = new List<T>();
        mediaDictionary = new Dictionary<string, T>();
    }

    public void Add(T item)
    {
        if (item == null)
        {
            throw new ArgumentNullException("Элемент не может иметь значение null");
        }

        if (mediaDictionary.ContainsKey(item.Title))
        {
            throw new InvalidOperationException($"Элемент с названием '{item.Title}' уже существует в библиотеке");
        }

        mediaList.Add(item);
        mediaDictionary.Add(item.Title, item);
        Console.WriteLine($"Добавлен: {item.GetInfo()}");
    }

    public bool Remove(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Название не может быть пустым");
        }

        if (!mediaDictionary.ContainsKey(title))
        {
            throw new KeyNotFoundException($"Элемент с названием '{title}' не найден в библиотеке");
        }

        T item = mediaDictionary[title];
        mediaList.Remove(item);
        mediaDictionary.Remove(title);
        Console.WriteLine($"Удален: {item.GetInfo()}");
        return true;
    }

    public T FindByTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Название не может быть пустым");
        }

        if (!mediaDictionary.ContainsKey(title))
        {
            throw new KeyNotFoundException($"Элемент с названием '{title}' не найден");
        }

        return mediaDictionary[title];
    }

    public IEnumerable<T> FilterByYear(uint year)
    {
        return mediaList.Where(item => item.YearPublished > year);
    }

    public IEnumerable<T> GetAllAvailable()
    {
        return mediaList.Where(item => item.IsAvailable);
    }

    public void PrintAll()
    {
        if (mediaList.Count == 0)
        {
            Console.WriteLine("Библиотека пуста");
            return;
        }

        Console.WriteLine("Библиотека: ");
        foreach (var item in mediaList)
        {
            Console.WriteLine(item.GetInfo());
        }
        Console.WriteLine($"Всего элементов: {mediaList.Count}");
    }

    public void ExportToFile(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Путь к файлу не может быть пустым");
        }

        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(mediaList, options);
            File.WriteAllText(path, json);
            Console.WriteLine($"Данные успешно экспортированы в файл: {path}");
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException($"Нет доступа для записи в файл '{path}': {ex.Message}");
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException($"Ошибка ввода-вывода при записи в файл '{path}': {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Неожиданная ошибка при экспорте данных: {ex.Message}");
        }
    }

    public void BorrowItem(string title)
    {
        T item;
        try
        {
            item = FindByTitle(title);
        }
        catch (KeyNotFoundException ex)
        {
            throw new InvalidOperationException($"Нельзя выдать несуществующий элемент: {ex.Message}");
        }

        if (!item.IsAvailable)
        {
            throw new InvalidOperationException($"Элемент '{title}' уже выдан и недоступен");
        }

        item.IsAvailable = false;
        Console.WriteLine($"Элемент '{title}' выдан");
    }

    public void ReturnItem(string title)
    {
        T item;
        try
        {
            item = FindByTitle(title);
        }
        catch (KeyNotFoundException ex)
        {
            throw new InvalidOperationException($"Нельзя вернуть несуществующий элемент: {ex.Message}", ex);
        }

        if (item.IsAvailable)
        {
            throw new InvalidOperationException($"Элемент '{title}' не был выдан");
        }

        item.IsAvailable = true;
        Console.WriteLine($"Элемент '{title}' возвращен");
    }

    public void FindBooksAfterDefiniteYear(uint year)
    {
        var result = mediaList.Where(m => m.YearPublished > year).ToList();
        if (result.Count == 0)
        {
            Console.WriteLine($"Нет элементов, выпущенных после {year} года");
            return;
        }

        Console.WriteLine($"Элементы, выпущенные после {year} года:");
        foreach (var item in result)
        {
            Console.WriteLine(item.GetInfo());
        }
    }

    public void GetListMoviesSortedByDuration()
    {
        var movies = mediaList.Where(m => m is Movie).Cast<Movie>().OrderBy(m => m.Duration).ToList();

        if (movies.Count == 0)
        {
            Console.WriteLine("Фильмы в библиотеке отсутствуют");
            return;
        }

        Console.WriteLine("Фильмы, отсортированные по длительности:");
        foreach (var movie in movies)
        {
            Console.WriteLine(movie.GetInfo());
        }
    }

    public void FindNotAvailableComponents()
    {
        var notAvailable = mediaList.Where(m => !m.IsAvailable).ToList();

        if (notAvailable.Count == 0)
        {
            Console.WriteLine("Все элементы доступны");
            return;
        }

        Console.WriteLine("Недоступные элементы:");
        foreach (var item in notAvailable)
        {
            Console.WriteLine(item.GetInfo());
        }
    }
}
