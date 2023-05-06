using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB;

namespace SP_AndreevLab1._1.model
{
    public interface IModel
    {
        string AnalyzerDoWhile(string code);
        string ASMAnd(int value1, int value2);
        List<string[]> AddRow(string[] newRow);
        List<string[]> DeleteRow(int deleteRow);
        List<string[]> UpgradeRow(int idRow, string[] upgradeRow);
        List<string[]> ReadCSV(string path);
        void WriteCSV(string path);
        List<string[]> ClearSelectedRow(int index);
        void FileOut();
        List<string[]> FileIn();
        IList<DB.DBFileInfo> connectBD();

        int disconnectBD();

        List<DB.DBFileInfo> AddDllFileDB();

        String deleteDllFileDB(int index);

        int clearAllDllFilesDB();
    }
}
