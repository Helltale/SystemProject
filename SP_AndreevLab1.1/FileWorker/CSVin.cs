using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace FileWorker
{
    public static class CSVin
    {
        public static List<string[]> ReadCSV(string path)
        {
            List<string[]> dataCSVin = new List<string[]>();

            using (TextFieldParser tfp = new TextFieldParser(path)) {
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters(",");

                while (!tfp.EndOfData) {
                    dataCSVin.Add(tfp.ReadFields());
                
                }
            }
            return dataCSVin;
        }

    }

    
}
