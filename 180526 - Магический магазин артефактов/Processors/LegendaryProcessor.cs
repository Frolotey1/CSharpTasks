using _180526___Магический_магазин_артефактов.Artifacts;
using _180526___Магический_магазин_артефактов.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace _180526___Магический_магазин_артефактов.Processors
{
    public class LegendaryProcessor : IDataProcessor<LegendaryArtifact>
    {
        public List<LegendaryArtifact> LoadData(string filePath)
        {
            List<LegendaryArtifact> artifacts = new List<LegendaryArtifact>();
            int currentId = 100;

            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Файл {filePath} не найден");
                    return artifacts;
                }

                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split('|');
                    if (parts.Length == 5)
                    {
                        string name = parts[0];
                        int powerLevel = int.Parse(parts[1]);
                        Rarity rarity = (Rarity)Enum.Parse(typeof(Rarity), parts[2]);
                        string curseDescription = parts[3];
                        bool isCursed = bool.Parse(parts[4]);

                        artifacts.Add(new LegendaryArtifact(currentId++, name, powerLevel, rarity, curseDescription, isCursed));
                    }
                    else
                    {
                        Console.WriteLine($"Неверный формат строки: {line}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке текстового файла: {ex.Message}");
            }

            return artifacts;
        }

        public void SaveData(List<LegendaryArtifact> data, string filePath)
        {
            try
            {
                List<string> lines = new List<string>();
                foreach (var artifact in data)
                {
                    lines.Add($"{artifact.Name}|{artifact.PowerLevel}|{artifact.Rarity}|{artifact.CurseDescription}|{artifact.IsCursed}");
                }
                File.WriteAllLines(filePath, lines);
                Console.WriteLine($"Данные сохранены в {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении текстового файла: {ex.Message}");
            }
        }
    }
}
