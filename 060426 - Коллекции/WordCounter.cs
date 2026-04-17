using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class WordCounter {
    public static void Run() {
        Console.WriteLine("Подсчёт слов в тексте");
        
        Console.Write("Введите текст: ");
        string text = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(text)) {
            Console.WriteLine("Текст пуст");
            return;
        }
       
        char[] delimiters = { ' ', ',', '.', '!', '?', ';', ':', '(', ')', '[', ']', '{', '}', '\n', '\t', '"', '\'', '-', '—', '…' };

        string[] words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

        Dictionary<string, int> wordCount = new Dictionary<string, int>();

        foreach (string word in words) {
            string lowerWord = word.ToLower();
            
            if (wordCount.ContainsKey(lowerWord)){
                wordCount[lowerWord]++;
            }
            else {
                wordCount[lowerWord] = 1;
            }
        }
        
        Console.WriteLine("Таблица частоты слов");
        
        foreach (var pair in wordCount) {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }

        Console.WriteLine($"Всего уникальных слов: {wordCount.Count}");
        Console.WriteLine($"Всего слов в тексте: {words.Length}");
    }
}
