using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300326___Интерфейсы
{
    public interface ISalary
    {
        string FullName { get; set; }
        int WorkingDays { get; set; }
        double CalculateSalary();
    }
}
