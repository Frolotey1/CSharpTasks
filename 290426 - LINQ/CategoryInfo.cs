using System;

namespace SmartWarehouse;

/*
Структура здесь уместна по следующим причинам:
1. Значимая семантика - структура является значимым типом, не имеет идентичности.
   Две категории с одинаковыми Name и Code считаются равными.
2. Неизменяемость - readonly структура, поля нельзя изменить после создания.
3. Отсутствие наследования - категории не требуется расширение функционала.
*/
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

    public override string ToString() {
        return Name + " (" + Code + ")";
    }
}
