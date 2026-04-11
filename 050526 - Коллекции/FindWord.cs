namespace Project;
using System;
using System.Collections.Generic;
using System.Linq; 

public class FindWord {
    public static void Run() {
        Dictionary<string,int> countWords = new Dictionary<string,int>();
        Console.WriteLine("Напишите любой текст: ");
        string text = Console.ReadLine();

        if(string.IsNullOrEmpty(text)) {
            Console.WriteLine("Текст пустой");
            return;
        }

        char[] delimiters = ['!',',','.',' ','?','#','(',')','-','+','%','@','*','&','$','^',':',';','[',']','<','>','|'];
        string[] dividedString = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

        foreach(var everyWord in dividedString) {
            string lowerWord = everyWord.ToLower();
            
            if(countWords.ContainsKey(lowerWord)) {
                countWords[lowerWord]++;
            } else {
                countWords[lowerWord] = 1;
            }
        }

        foreach(var result in countWords) {
            Console.WriteLine($"{result.Key}: {result.Value}");
        }
    }
}
