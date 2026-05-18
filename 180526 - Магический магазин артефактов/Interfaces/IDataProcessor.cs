using _180526___Магический_магазин_артефактов.Artifacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _180526___Магический_магазин_артефактов.Interfaces
{
    public interface IDataProcessor<T> where T : Artifact
    {
        List<T> LoadData(string filePath);
        void SaveData(List<T> data, string filePath);
    }
}
