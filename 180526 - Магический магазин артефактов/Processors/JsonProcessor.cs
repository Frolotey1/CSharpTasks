using _180526___Магический_магазин_артефактов.Artifacts;
using _180526___Магический_магазин_артефактов.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class JsonProcessor : IDataProcessor<ModernArtifact>
{
    public List<ModernArtifact> LoadData(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл {filePath} не найден");
                return new List<ModernArtifact>();
            }

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<ModernArtifact>>(json) ?? new List<ModernArtifact>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке JSON: {ex.Message}");
            return new List<ModernArtifact>();
        }
    }

    public void SaveData(List<ModernArtifact> data, string filePath)
    {
        try
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, json);
            Console.WriteLine($"Данные сохранены в {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении JSON: {ex.Message}");
        }
    }
}