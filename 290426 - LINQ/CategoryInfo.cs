using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
Структура здесь уместна по следующим причинам:
1. Значимая семантика - структура является значимым типом и поэтому не имеет идентичности. Поля нельзя различить между собой. 
2. Неизменяемость - readonly структура. Значения полей нельзя изменить в отдельных методах, только максимум в конструкторе класса либо структуры. 
3. Отсутствие наследования - категории не нужно расширение функционала. 
*/

namespace _290426___LINQ {
    public readonly struct CategoryInfo {
        public string Name { get; }
        public string Code { get; }

        public CategoryInfo(string name, string code) {
            if (string.IsNullOrWhiteSpace(name)) {
                Console.WriteLine("Ошибка: название категории не может быть пустым");
                Name = "Unknown";
                Code = "UNK";
                return;
            }

            if (string.IsNullOrWhiteSpace(code)) {
                Console.WriteLine("Ошибка: код категории не может быть пустым");
                Name = name;
                Code = "UNK";
                return;
            }

            Name = name;
            Code = code.ToUpper();
        }

        public override string ToString()  {
            return $"{Name} ({Code})";
        }
    }
}
