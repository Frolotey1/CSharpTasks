using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300326___Интерфейсы
{
    public interface IConverter
    {
        string FromScale { get; set; }
        string ToScale { get; set; }
        double Convert(double value);
    }
}
