using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_AndreevLab1._1.errors
{
    internal class Errors : IErrors
    {
        public void Show(string message, string nameMessage = "Ошибка")
        {
            MessageBox.Show(message, nameMessage);
        }
    }
}
