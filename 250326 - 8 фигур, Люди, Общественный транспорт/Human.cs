namespace Project;
using System;

public class Human {
    public string Name {get; set;}
    public uint Age {get; set;}
    public string info() {
	return string.Format("Имя: {0} | Возраст: {1}",Name,Age);
    }
}
