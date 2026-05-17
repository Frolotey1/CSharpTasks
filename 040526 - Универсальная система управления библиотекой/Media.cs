namespace Project;
using System;

public class Media {
    public string Title { get; set; }
    public string Author { get; set; }
    public uint YearPublished { get; set; }
    public bool IsAvailable { get; set; } = true;

    public Media(string title, string author, uint yearPublished) {
        Title = title;
        Author = author;
        YearPublished = yearPublished;
    }

    public virtual string GetInfo() {
        return $"Название: {Title} | Автор: {Author} | Год: {YearPublished} | Доступно: {(IsAvailable ? "Да" : "Нет")}";
    }
}
