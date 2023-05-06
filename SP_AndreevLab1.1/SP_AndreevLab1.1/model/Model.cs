using Analyzer;
using DllAnd;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FileWorker;
using DllAnd;
using Analyzer;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DB;

namespace SP_AndreevLab1._1.model
{
    internal class Model : IModel
    {   
        DGVwork dgvwork = new DGVwork();
        DBMain dbmain = new DBMain();
        private object _instance;
        private MethodInfo _method;

        public Model() {
            
            dgvwork = new DGVwork();

            if (!File.Exists("logAnd.dll"))
            {
                BitwiseAnd.ASMAnd();
            }
            string modName = "logAnd.dll";
            string typeName = "AND";
            Assembly asm = Assembly.Load(File.ReadAllBytes(modName));
            Type t = asm.GetType(typeName);
            _method = t.GetMethod("AND", BindingFlags.Instance | BindingFlags.Public);
            _instance = Activator.CreateInstance(t);
        }


        //сделано: анализатор, асм, file
        public string AnalyzerDoWhile(string code) { return AnalyzerMain.Analyz(code); }

        public string ASMAnd(int value1, int value2)
        {
            try
            {
                return _method.Invoke(_instance, new object[] { value1, value2 }).ToString();
            }
            catch
            {
                throw new Exception("Ошибка. выход за границы Integer;");
            }
        }

        public List<string[]> AddRow(string[] newRow) {
            return dgvwork.AddRow(newRow);
        }

        public List<string[]> DeleteRow(int deleteRow) { 
            return dgvwork.DeleteRow(deleteRow);
        }

        public List<string[]> UpgradeRow(int idRow, string[] upgradeRow) { 
            return dgvwork.UpgradeRow(idRow, upgradeRow);
        }

        public List<string[]> ReadCSV(string path) { 
            return dgvwork.ReadCSV(path);
        }

        public void WriteCSV(string path) {
            dgvwork.WriteCSV(path);
        }

        public List<string[]> ClearSelectedRow(int index) {
            return dgvwork.ClearSelectedRow(index);
        }

        public void FileOut()
        {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "out.csv";
                bool fileError = false;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try { File.Delete(sfd.FileName); } catch (IOException ex) { fileError = true; }
                    }
                    if (!fileError)
                    {
                        WriteCSV(sfd.FileName);
                    }
                }
            
        }

        public List<string[]> FileIn()
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = "C:\\Users\\pizdn\\source\\repos\\SP_AndreevLab1.1\\SP_AndreevLab1.1\\assets";
                ofd.Filter = "csv files (*.csv)|*.csv|txt files (*.txt)|*.txt|All files (*.*)|*.*";
                string filepath;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    filepath = ofd.FileName;
                }
                else { return null; }

                List<string[]> result = ReadCSV(filepath);
                return result;
            }
            catch
            {
                return null;
            }

        }

        public IList<DBFileInfo> connectBD()
        {
            return dbmain.connect();
        }

        public int disconnectBD()
        {
            return dbmain.disconnect();
        }

        public List<DB.DBFileInfo> AddDllFileDB()
        {
            return dbmain.AddElement();
        }

        public String deleteDllFileDB(int index)
        {
            return dbmain.DeleteElement(index);
        }

        public int clearAllDllFilesDB()
        {
            return dbmain.DeleteAllElements();
        }

    }

    
}
