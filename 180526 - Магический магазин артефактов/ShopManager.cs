using _180526___Магический_магазин_артефактов.Artifacts;
using _180526___Магический_магазин_артефактов.Processors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ShopManager
{
    private List<Artifact> _allArtifacts = new List<Artifact>();

    public void LoadAllData()
    {
        Console.WriteLine("Загрузка данных из файлов");

        string dataDir = "Data";
        if (!Directory.Exists(dataDir))
        {
            Directory.CreateDirectory(dataDir);
            Console.WriteLine($"Создана папка {dataDir}");
        }

        string xmlPath = Path.Combine(dataDir, "antique.xml");
        if (!File.Exists(xmlPath))
        {
            CreateSampleAntiqueArtifacts(xmlPath);
        }
        XmlProcessor xmlProcessor = new XmlProcessor();
        var antiqueArtifacts = xmlProcessor.LoadData(xmlPath);
        _allArtifacts.AddRange(antiqueArtifacts);
        Console.WriteLine($"Загружено античных артефактов: {antiqueArtifacts.Count}");

        string jsonPath = Path.Combine(dataDir, "modern.json");
        if (!File.Exists(jsonPath))
        {
            CreateSampleModernArtifacts(jsonPath);
        }
        JsonProcessor jsonProcessor = new JsonProcessor();
        var modernArtifacts = jsonProcessor.LoadData(jsonPath);
        _allArtifacts.AddRange(modernArtifacts);
        Console.WriteLine($"Загружено современных артефактов: {modernArtifacts.Count}");

        string txtPath = Path.Combine(dataDir, "legends.txt");
        if (!File.Exists(txtPath))
        {
            CreateSampleLegendaryArtifacts(txtPath);
        }
        LegendaryProcessor legendaryProcessor = new LegendaryProcessor();
        var legendaryArtifacts = legendaryProcessor.LoadData(txtPath);
        _allArtifacts.AddRange(legendaryArtifacts);
        Console.WriteLine($"Загружено легендарных артефактов: {legendaryArtifacts.Count}");

        Console.WriteLine($"\nВсего артефактов: {_allArtifacts.Count}");
    }

    private void CreateSampleAntiqueArtifacts(string filePath)
    {
        var samples = new List<AntiqueArtifact>
        {
            new AntiqueArtifact(1, "Amulet of Yendor", 95, Rarity.Legendary, 1200, "Arcadia"),
            new AntiqueArtifact(2, "Staff of Ages", 78, Rarity.Epic, 850, "Avalon"),
            new AntiqueArtifact(3, "Cloak of Invisibility", 60, Rarity.Rare, 450, "Shadowfell")
        };
        XmlProcessor xmlProcessor = new XmlProcessor();
        xmlProcessor.SaveData(samples, filePath);
        Console.WriteLine($"Создан пример файла: {filePath}");
    }

    private void CreateSampleModernArtifacts(string filePath)
    {
        var samples = new List<ModernArtifact>
        {
            new ModernArtifact(10, "Hyper Phase Blaster", 88, Rarity.Epic, 9.5, "TechMage Inc."),
            new ModernArtifact(11, "Neural Enhancer", 72, Rarity.Rare, 7.8, "NeuroTech Labs"),
            new ModernArtifact(12, "Quantum Displacer", 95, Rarity.Legendary, 9.9, "Arcane Systems")
        };
        JsonProcessor jsonProcessor = new JsonProcessor();
        jsonProcessor.SaveData(samples, filePath);
        Console.WriteLine($"Создан пример файла: {filePath}");
    }

    private void CreateSampleLegendaryArtifacts(string filePath)
    {
        var samples = new List<LegendaryArtifact>
        {
            new LegendaryArtifact(100, "Sword of Destiny", 100, Rarity.Legendary, "Drains life from the wielder", true),
            new LegendaryArtifact(101, "Cursed Amulet", 45, Rarity.Rare, "Slowly turns wielder into stone", true),
            new LegendaryArtifact(102, "Blessed Shield", 70, Rarity.Epic, "Protects from dark magic", false)
        };
        LegendaryProcessor legendaryProcessor = new LegendaryProcessor();
        legendaryProcessor.SaveData(samples, filePath);
        Console.WriteLine($"Создан пример файла: {filePath}");
    }

    public void AddAntiqueArtifactInteractive()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("Добавление античного артефакта");

            Console.Write("Введите название: ");
            string name = Console.ReadLine();

            Console.Write("Введите уровень силы (число): ");
            int power = int.Parse(Console.ReadLine());

            Console.Write("Введите редкость (Common, Rare, Epic, Legendary): ");
            Rarity rarity = (Rarity)Enum.Parse(typeof(Rarity), Console.ReadLine(), true);

            Console.Write("Введите возраст (лет): ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Введите измерение происхождения: ");
            string origin = Console.ReadLine();

            int newId = _allArtifacts.Count + 1000;
            AntiqueArtifact artifact = new AntiqueArtifact(newId, name, power, rarity, age, origin);
            _allArtifacts.Add(artifact);
            Console.WriteLine($"\nАртефакт '{name}' успешно добавлен!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при добавлении: {ex.Message}");
        }
    }

    public void AddModernArtifactInteractive()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("Добавление современного артефакта");

            Console.Write("Введите название: ");
            string name = Console.ReadLine();

            Console.Write("Введите уровень силы (число): ");
            int power = int.Parse(Console.ReadLine());

            Console.Write("Введите редкость (Common, Rare, Epic, Legendary): ");
            Rarity rarity = (Rarity)Enum.Parse(typeof(Rarity), Console.ReadLine(), true);

            Console.Write("Введите технологический уровень (число): ");
            double techLevel = double.Parse(Console.ReadLine());

            Console.Write("Введите производителя: ");
            string manufacturer = Console.ReadLine();

            int newId = _allArtifacts.Count + 1000;
            ModernArtifact artifact = new ModernArtifact(newId, name, power, rarity, techLevel, manufacturer);
            _allArtifacts.Add(artifact);
            Console.WriteLine($"\nАртефакт '{name}' успешно добавлен!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при добавлении: {ex.Message}");
        }
    }

    public void AddLegendaryArtifactInteractive()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("Добавление легендарного артефакта");

            Console.Write("Введите название: ");
            string name = Console.ReadLine();

            Console.Write("Введите уровень силы (число): ");
            int power = int.Parse(Console.ReadLine());

            Console.Write("Введите редкость (Common, Rare, Epic, Legendary): ");
            Rarity rarity = (Rarity)Enum.Parse(typeof(Rarity), Console.ReadLine(), true);

            Console.Write("Введите описание проклятия: ");
            string curse = Console.ReadLine();

            Console.Write("Проклят? (true/false): ");
            bool isCursed = bool.Parse(Console.ReadLine());

            int newId = _allArtifacts.Count + 1000;
            LegendaryArtifact artifact = new LegendaryArtifact(newId, name, power, rarity, curse, isCursed);
            _allArtifacts.Add(artifact);
            Console.WriteLine($"Артефакт '{name}' успешно добавлен!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при добавлении: {ex.Message}");
        }
    }

    public void ShowAllArtifacts()
    {
        Console.WriteLine("Все артефакты");
        if (_allArtifacts.Count == 0)
        {
            Console.WriteLine("Нет артефактов");
            return;
        }

        foreach (var artifact in _allArtifacts)
        {
            Console.WriteLine(artifact.Serialize());
        }
        Console.WriteLine($"Всего артефактов: {_allArtifacts.Count}");
    }

    public void GenerateReport()
    {
        Console.WriteLine(" Отчёт по магазину");

        if (_allArtifacts.Count == 0)
        {
            Console.WriteLine("Нет данных для отчёта");
            return;
        }

        var rarityStats = _allArtifacts
            .GroupBy(a => a.Rarity)
            .Select(g => new
            {
                Rarity = g.Key,
                Count = g.Count(),
                AvgPower = g.Average(a => a.PowerLevel),
                MinPower = g.Min(a => a.PowerLevel),
                MaxPower = g.Max(a => a.PowerLevel)
            })
            .OrderByDescending(s => s.AvgPower);

        List<string> reportLines = new List<string>();
        reportLines.Add("=== ОТЧЁТ МАГАЗИНА АРТЕФАКТОВ ===");
        reportLines.Add($"Дата отчёта: {DateTime.Now}");
        reportLines.Add($"Всего артефактов: {_allArtifacts.Count}");
        reportLines.Add("");

        foreach (var stat in rarityStats)
        {
            reportLines.Add($"Редкость: {stat.Rarity}");
            reportLines.Add($"  Количество: {stat.Count}");
            reportLines.Add($"  Средняя сила: {stat.AvgPower:F2}");
            reportLines.Add($"  Минимальная сила: {stat.MinPower}");
            reportLines.Add($"  Максимальная сила: {stat.MaxPower}");
            reportLines.Add("");
        }

        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string reportPath = Path.Combine(baseDirectory, "Data", "report.txt");

        string dataDir = Path.GetDirectoryName(reportPath);
        if (!Directory.Exists(dataDir))
        {
            Directory.CreateDirectory(dataDir);
            Console.WriteLine($"Создана папка: {dataDir}");
        }

        File.WriteAllLines(reportPath, reportLines);
        Console.WriteLine($"Отчёт сохранён в {reportPath}");
    }

    public List<Artifact> FindCursedArtifacts()
    {
        return _allArtifacts
            .Where(a => a is LegendaryArtifact legendary && legendary.IsCursed && a.PowerLevel > 50)
            .ToList();
    }
    public IEnumerable<dynamic> GroupByRarity()
    {
        return _allArtifacts
            .GroupBy(a => a.Rarity)
            .Select(g => new
            {
                Rarity = g.Key,
                Count = g.Count(),
                Artifacts = g.ToList()
            })
            .OrderByDescending(g => g.Count);
    }

    public List<Artifact> TopByPower(int count)
    {
        return _allArtifacts
            .OrderByDescending(a => a.PowerLevel)
            .Take(count)
            .ToList();
    }

    public void ExportToJson(string filePath)
    {
        try
        {
            string json = JsonConvert.SerializeObject(_allArtifacts, Formatting.Indented);
            string dataDir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dataDir))
                Directory.CreateDirectory(dataDir);
            File.WriteAllText(filePath, json);
            Console.WriteLine($"Данные экспортированы в {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при экспорте: {ex.Message}");
        }
    }

    public void ExportToXml(string filePath)
    {
        try
        {
            var antique = _allArtifacts.OfType<AntiqueArtifact>().ToList();
            string dataDir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dataDir))
                Directory.CreateDirectory(dataDir);
            XmlProcessor xmlProcessor = new XmlProcessor();
            xmlProcessor.SaveData(antique, filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при экспорте XML: {ex.Message}");
        }
    }
}