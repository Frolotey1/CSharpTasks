namespace Project;
using System;
using System.Collections.Generic;

public interface IMediaManager<T> where T : Media
{
    void Add(T item);
    bool Remove(string title);
    T FindByTitle(string title);
    IEnumerable<T> FilterByYear(uint year);
    IEnumerable<T> GetAllAvailable();
    void PrintAll();
    void ExportToFile(string path);
}
