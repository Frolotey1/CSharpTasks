using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _180526___Магический_магазин_артефактов.Interfaces
{
    public interface IExportable
    {
        string ExportToJson();
        string ExportToXml();
    }
}
