namespace Project;
using System;
using System.Collections.Generic;

public struct Odds {
    private int A { get; set; }
    private int B { get; set; }
    
    public void Parse(string getOdds) {
        if (getOdds.Length == 0) {
            Console.WriteLine("Строка пустая");
        } else {
            string[] tokens = getOdds.Split(' ');
            if (tokens.Length < 2 || tokens.Length > 2) {
                Console.WriteLine("Нужно ввести 2 коэффициента, не больше и не меньше");
            } else {
                A = int.Parse(tokens[0]);
                B = int.Parse(tokens[1]);
            }
        }
    }
    
    public (int E1, int E2) Expression() {
        int X = 0, Y = 0, repeate = 0;
        int E1 = 0, E2 = 0;
        
        while (repeate < 2) {
            Console.Write("Введите значение X: ");
            X = int.Parse(Console.ReadLine());
            Console.Write("Введите значение Y: ");
            Y = int.Parse(Console.ReadLine());
            
            int E = A * X + B * Y;
            
            if (repeate == 0)
                E1 = E;
            else
                E2 = E;
            
            repeate++;
        }
        
        return (E1, E2);
    }
}
