using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Runtime.Remoting.Messaging;

namespace FileWorker
{
    public static class CSVout
    {
        public static void WriteCSV(List<string[]> data, string path)
        {
            using (StreamWriter file = new StreamWriter(path))
            {
                foreach (string[] result in data){
                        file.WriteLine(result[0] +"," + result[1] + ","+ result[2]);
                }

                
            }
        }
    }
}
