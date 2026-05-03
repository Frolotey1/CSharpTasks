namespace Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

public struct ShowFilm {
    private string _filmname;
    private string _room;
    private string _showdate;
    private uint _countplaces;
    private double _cost;

    public ShowFilm(string filmName, string room, string showDate, uint countPlaces, double cost) {
        _filmname = filmName;
        _room = room;
        _showdate = showDate;
        _countplaces = countPlaces;
        _cost = cost;
    }

    public string GetFilmname() => _filmname;
    public string GetShowdate() => _showdate;

    public string Info => $"Название фильма: {_filmname} | Зал показа: {_room} | Дата показа: {_showdate} | Количество мест: {_countplaces} | Стоимость билета: {_cost} руб.";

    public double Salary(uint? countPeople = null) {
        if (countPeople == null)
            return _countplaces * _cost;
        else
            return countPeople.Value * _cost;
    }

    public static void Run() {
        List<ShowFilm> films = new List<ShowFilm>();

        for (int i = 0; i < 5; i++) {
            Console.Write("Введите название фильма: ");
            string filmname = Console.ReadLine();
            Console.Write("Введите зал показа: ");
            string room = Console.ReadLine();
            Console.Write("Введите дату и время показа: ");
            string showdate = Console.ReadLine();
            Console.Write("Введите количество мест в зале: ");
            uint countplaces = uint.Parse(Console.ReadLine());
            Console.Write("Введите стоимость билета: ");
            double cost = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.WriteLine();

            films.Add(new ShowFilm(filmname, room, showdate, countplaces, cost));
        }

        Console.WriteLine("Текущий список фильмов:");
        foreach (var film in films)
            Console.WriteLine(film.Info);

        Console.WriteLine();
        Console.Write("Введите название фильма для добавления: ");
        string newFilmname = Console.ReadLine();
        Console.Write("Введите зал показа: ");
        string newRoom = Console.ReadLine();
        Console.Write("Введите дату и время показа: ");
        string newShowdate = Console.ReadLine();
        Console.Write("Введите количество мест в зале: ");
        uint newCountplaces = uint.Parse(Console.ReadLine());
        Console.Write("Введите стоимость билета: ");
        double newCost = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

        films.Add(new ShowFilm(newFilmname, newRoom, newShowdate, newCountplaces, newCost));

        var sortedFilms = films.OrderBy(f => f.GetFilmname()).ToList();

        Console.WriteLine("Отсортированный список по названию фильма:");
        foreach (var film in sortedFilms)
            Console.WriteLine(film.Info);

        Console.WriteLine();
        Console.Write("Введите название фильма для удаления: ");
        string searchName = Console.ReadLine();
        Console.Write("Введите дату и время показа для удаления: ");
        string searchDate = Console.ReadLine();

        ShowFilm? toRemove = null;
        foreach (var film in films) {
            if (film.GetFilmname() == searchName && film.GetShowdate() == searchDate) {
                toRemove = film;
                break;
            }
        }

        if (toRemove != null) {
            films.Remove(toRemove.Value);
            Console.WriteLine("Экземпляр удалён");
        }
        else
            Console.WriteLine("Экземпляр не найден");

        Console.WriteLine("Итоговый список фильмов:");
        foreach (var film in films.OrderBy(f => f.GetFilmname()))
            Console.WriteLine(film.Info);

        Console.WriteLine();
        Console.Write("Введите количество пришедших людей (по нажатию Enter отобразится полный зал): ");
        string input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input)) {
            Console.WriteLine("Заработок с полного зала: " + films[0].Salary() + " рублей");
        }
        else {
            uint people = uint.Parse(input);
            Console.WriteLine("Заработок с показа: " + films[0].Salary(people) + " рублей");
        }
    }
}
