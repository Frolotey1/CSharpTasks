using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300326___Интерфейсы
{
    public interface ICafeClient
    {
        static int HourlyRate { get; set; }
        string FullName { get; set; }
        int Hours { get; set; }
        double TotalCost();
    }
}
