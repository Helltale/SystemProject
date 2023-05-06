using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWorker
{
    public class DGVwork
    {
        List<string[]> data = new List<string[]>();

        public List<string[]> DeleteRow(int deleteRow) {
            for (int i = 0; i < data.Count; i++){
                data.RemoveAt(deleteRow);
            }
            return data;
        } 

        public List<string[]> AddRow(string[] newRow) {
            data.Add(newRow);
            return data;
        }

        public List<string[]> UpgradeRow(int idRow, string[] upgradeRow) {
            for (int i = 0; i < data.Count; i++) { 
                data[idRow] = upgradeRow;
            }
            return data;
        }

        public void WriteCSV(string path) {
            CSVout.WriteCSV(data, path);
        }

        public List<string[]> ReadCSV(string path) {
            data = CSVin.ReadCSV(path);
            return CSVin.ReadCSV(path);
        }

        public List<string[]> ClearSelectedRow(int index) { 
            data.RemoveAt(index);
            return data;
        }

    }
}
