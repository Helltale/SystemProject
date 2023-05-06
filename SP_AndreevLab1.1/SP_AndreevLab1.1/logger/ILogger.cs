using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_AndreevLab1._1.logger
{
    internal interface ILogger
    {
        void Show(string message, string name = "Ошибка");
    }
}
