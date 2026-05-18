using _180526___Магический_магазин_артефактов.Artifacts;
using _180526___Магический_магазин_артефактов.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace _180526___Магический_магазин_артефактов.Processors
{
    public class XmlProcessor : IDataProcessor<AntiqueArtifact>
    {
        public List<AntiqueArtifact> LoadData(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Файл {filePath} не найден");
                    return new List<AntiqueArtifact>();
                }

                XmlSerializer serializer = new XmlSerializer(typeof(List<AntiqueArtifact>));
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    return (List<AntiqueArtifact>)serializer.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке XML: {ex.Message}");
                return new List<AntiqueArtifact>();
            }
        }

        public void SaveData(List<AntiqueArtifact> data, string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<AntiqueArtifact>));
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    serializer.Serialize(fs, data);
                }
                Console.WriteLine($"Данные сохранены в {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении XML: {ex.Message}");
            }
        }
    }
}
