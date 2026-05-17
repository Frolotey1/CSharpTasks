namespace Project;
using System;

public class Book : Media {
    public uint CountPages { get; set; }
    public string Genre { get; set; }

    public Book(string title, string author, uint yearPublished, uint countPages, string genre)
        : base(title, author, yearPublished) {
        CountPages = countPages;
        Genre = genre;
    }

    public override string GetInfo() {
        return base.GetInfo() + $" | Жанр: {Genre} | Страниц: {CountPages}";
    }
}
