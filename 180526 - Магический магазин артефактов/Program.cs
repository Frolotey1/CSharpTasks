using _180526___Магический_магазин_артефактов;
using System;
using System.Linq;

public class Program
{
    private static ShopManager shop = new ShopManager();

    public static void Main()
    {
        Console.WriteLine("МАГИЧЕСКИЙ МАГАЗИН АРТЕФАКТОВ");

        shop.LoadAllData();

        while (true)
        {
            Console.WriteLine("1) Добавить античный артефакт");
            Console.WriteLine("2) Добавить современный артефакт");
            Console.WriteLine("3) Добавить легендарный артефакт");
            Console.WriteLine("4) Показать все артефакты");
            Console.WriteLine("5) Показать проклятые артефакты (PowerLevel > 50)");
            Console.WriteLine("6) Группировка по редкости");
            Console.WriteLine("7) Топ-5 артефактов по силе");
            Console.WriteLine("8) Сгенерировать отчёт");
            Console.WriteLine("9) Экспорт всех данных в JSON");
            Console.WriteLine("10) Экспорт античных артефактов в XML");
            Console.WriteLine("11) Выход");
            Console.Write("\nВыберите действие: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    shop.AddAntiqueArtifactInteractive();
                    break;
                case 2:
                    shop.AddModernArtifactInteractive();
                    break;
                case 3:
                    shop.AddLegendaryArtifactInteractive();
                    break;
                case 4:
                    shop.ShowAllArtifacts();
                    break;
                case 5:
                    ShowCursedArtifacts();
                    break;
                case 6:
                    ShowGroupByRarity();
                    break;
                case 7:
                    ShowTopByPower();
                    break;
                case 8:
                    shop.GenerateReport();
                    break;
                case 9:
                    shop.ExportToJson("Data/export_all.json");
                    break;
                case 10:
                    shop.ExportToXml("Data/export_antique.xml");
                    break;
                case 11:
                    Console.WriteLine("Завершение программы");
                    return;
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
        }
    }

    private static void ShowCursedArtifacts()
    {
        var cursed = shop.FindCursedArtifacts();
        Console.WriteLine($"Проклятые артефакты (PowerLevel > 50)");
        if (cursed.Count == 0)
        {
            Console.WriteLine("Нет проклятых артефактов с силой выше 50");
            return;
        }
        foreach (var artifact in cursed)
        {
            Console.WriteLine(artifact.Serialize());
        }
    }

    private static void ShowGroupByRarity()
    {
        var grouped = shop.GroupByRarity();
        Console.WriteLine($"Группировка по редкости");
        foreach (var group in grouped)
        {
            Console.WriteLine($"{group.Rarity}: {group.Count} шт.");
            foreach (var artifact in group.Artifacts)
            {
                Console.WriteLine($"  - {artifact.Name} (сила: {artifact.PowerLevel})");
            }
            Console.WriteLine();
        }
    }

    private static void ShowTopByPower()
    {
        var top = shop.TopByPower(5);
        Console.WriteLine($"Топ-5 артефактов по силе");
        if (top.Count == 0)
        {
            Console.WriteLine("Нет артефактов");
            return;
        }
        foreach (var artifact in top)
        {
            Console.WriteLine(artifact.Serialize());
        }
    }
}