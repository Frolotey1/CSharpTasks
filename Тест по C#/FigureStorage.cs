namespace Project;
using System;

public class FigureStorage {
    private Figure[] _figures;
    uint size;
    uint index;
    public FigureStorage(uint Size) {
        this.size = Size;
        _figures = new Figure[this.size];
        index = 0;
    }
    public void AddFigure(Figure f) { 
        if(index >= this.size) {
            Console.WriteLine("Массив переполнен");
        } else {
            _figures[index] = f;
            index++;
        }
    }
    public Figure[] GetAll() {
        return _figures;
    }
    public void GetTotalArea() {
        double sumArea = 0.0;
        foreach(var figure in _figures) {
            if (figure != null) {
                sumArea += figure.GetArea();
            }
        }
        Console.WriteLine($"Суммарная площадь всех фигур: {sumArea}");
    }
}
