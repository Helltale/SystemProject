using NHibernate;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using System.Data;
using System.IO;


namespace DB
{
    public class DBMain
    {
        ISession s = null;
        IList<DBFileInfo> DBFiles = new List<DBFileInfo>();
        


        private List<string[]> openDllFile()
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

        public List<string[]> ReadCSV(string path)
        {
            List<string[]> dataCSVin = new List<string[]>();

            using (TextFieldParser tfp = new TextFieldParser(path))
            {
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters(",");

                while (!tfp.EndOfData)
                {
                    dataCSVin.Add(tfp.ReadFields());

                }
            }
            return dataCSVin;
        }

        public IList<DBFileInfo> connect()
        {
            s = DBSession.OpenSession();
            DBFiles = s.CreateCriteria<DBFileInfo>().List<DBFileInfo>();
            return DBFiles;
        }

        public int disconnect()
        {
            if (s != null)
            {
                DBSession.CloseSession(s);
                s = null;
                DBFiles.Clear();
            }
            return DBFiles.Count;
        }

        public List<DB.DBFileInfo> AddElement()
        {
            List <string[]> tmp = openDllFile();
            List<DB.DBFileInfo> tmp1 = new List<DB.DBFileInfo>();
           

            using (var tx = s.BeginTransaction())
            {
                foreach (string[] res in tmp)
                {
                    var fileDB = new DBFileInfo()
                    {
                        path = res[0],
                        size = res[1],
                        date_ = res[2]

                    };
                    s.Save(fileDB);
                    tmp1.Add(fileDB);


                }
                tx.Commit();
                //DBFiles.Add(fileDB);
                return tmp1;
            }
        }

        public System.String DeleteElement(int index)
        {
            if (s != null)
            {
                using (var tx = s.BeginTransaction())
                {
                    var files = s.CreateCriteria<DBFileInfo>().List<DBFileInfo>();
                    string name = files[index].path;
                    s.Delete(files[index]);
                    DBFiles.RemoveAt(index);
                    tx.Commit();

                    return name;
                }
            }
            return null;
        }

        public int DeleteAllElements()
        {
            if (s != null)
            {
                using (var tx = s.BeginTransaction())
                {
                    int amount = s.CreateCriteria<DBFileInfo>().List<DBFileInfo>().Count;
                    s.CreateQuery("DELETE FROM DBFileInfo").ExecuteUpdate();
                    tx.Commit();

                    return amount;
                }
            }
            return 0;
        }
    }
}
