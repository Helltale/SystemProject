using SP_AndreevLab1._1.model;
using SP_AndreevLab1._1.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Analyzer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection;
using DllAnd;
using System.Windows.Markup;
using System.Windows.Forms.VisualStyles;

namespace SP_AndreevLab1._1
{
    public partial class FormMain : Form, IView
    {

        private string filepath;
        private IView _view;
        private IModel _model = new Model();
        List<string[]> result;
        private object _instance;
        private MethodInfo _method;

        public FormMain()
        {
            InitializeComponent();
            panelInfo.Visible = false;
            panelFile.Visible= false;
            panelFunc.Visible= false;
            panelASM.Visible= false;
            panelDB.Visible= false;
            labelFunc1.Visible= false;
            labelFuncBool.Visible= false;
            LoggerMess = "Программа запущена";
        }

        public void HidePanel() {
            panelInfo.Visible = false;
            panelFile.Visible = false;
            panelFunc.Visible = false;
            panelASM.Visible = false;
            panelDB.Visible = false;
            labelHeader.Text = "Добро пожаловать";
        }
        public void OpenPanel(Panel panel, object sender) {
            if (panel != null) {
                panel.Visible= true;
                panel.Dock = DockStyle.Fill;
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            HidePanel();
            OpenPanel(panelInfo, null);
            labelHeader.Text = "Задание по варианту";
        }

        private void buttonFile_Click(object sender, EventArgs e)
        {
            HidePanel();
            OpenPanel(panelFile, null);
            labelHeader.Text = "Работа с файлом";
        }

        private void buttonFunc_Click(object sender, EventArgs e)
        {
            HidePanel();
            OpenPanel(panelFunc, null);
            labelHeader.Text = "Функция";
        }

        private void buttonASM_Click(object sender, EventArgs e)
        {
            HidePanel();
            OpenPanel(panelASM, null);
            labelHeader.Text = "Низкоуровневая вставка";
        }

        private void buttonDB_Click(object sender, EventArgs e)
        {
            HidePanel();
            OpenPanel(panelDB, null);
            labelHeader.Text = "База данных";
        }

        private void buttonReturnIndex_Click(object sender, EventArgs e)
        {
            HidePanel();
        }

        public void UploadDGV(List<string[]> result) {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Путь");
            dataTable.Columns.Add("Размер");
            dataTable.Columns.Add("Дата");

            foreach (string[] res in result) {
                dataTable.Rows.Add(res[0], res[1], res[2]);
            }
            dataGridViewFile.DataSource = dataTable;
        }

        public List<string[]> Upload {
            set {
                UploadDGV(value);
            }
        }

        private void buttonFileInput_Click(object sender, EventArgs e)
        {
            GetFileInputButtonEvent?.Invoke(this, EventArgs.Empty);
            
        }

        private void buttonFileOutput_Click(object sender, EventArgs e)
        {
            GetFileOutputButtonEvent?.Invoke(this, EventArgs.Empty);
        }

        public void ClearDGV(DataGridView dataGridViewFile) {
            while (dataGridViewFile.Rows.Count != 0) { dataGridViewFile.Rows.Remove(dataGridViewFile.Rows[dataGridViewFile.Rows.Count - 1]); }
        }

        private void buttonGridClear_Click(object sender, EventArgs e)
        {
            ClearDGV(dataGridViewFile);
        }

        private void buttonGridRowClear_Click(object sender, EventArgs e)
        {
            GetGridRowClear?.Invoke(this, EventArgs.Empty);
        }

        private void buttonGridRowAdd_Click(object sender, EventArgs e)
        {
            GetGridRowAdd?.Invoke(this, EventArgs.Empty);
        }

        private void buttonGridRowChange_Click(object sender, EventArgs e)
        {
            GetGridRowChange?.Invoke(this, EventArgs.Empty);
        }

        private void buttonDoAnalyze_Click(object sender, EventArgs e)
        {
            GetAnalyzeCode?.Invoke(this, EventArgs.Empty);
            
        }

        private void buttonDoASM_Click(object sender, EventArgs e)
        {
            GetASMFuntion?.Invoke(this, EventArgs.Empty);
        }


        public string Cycle { get => richTextBoxFunc.Text; }
        public string AnalyzerRes1 { set => labelFuncNumbers.Text = value; }
        public int value1 { get => Int32.Parse(numericUpDown1.Value.ToString()); }
        public int value2 { get => Int32.Parse(numericUpDown2.Value.ToString()); }
        public string ASMRes { set => textBoxASMres.Text = value; }
        public string[] DataText { get { return new string[] {textBoxFilePath.Text, textBoxFileSize.Text, textBoxFileDate.Text}; }  }
        public int GetID { get => dataGridViewFile.SelectedRows[0].Index; }
        public List<string[]> ListResult { set { UploadDGV(value); } }
        public List<DB.DBFileInfo> ListResultDB
        {
            set
            {
                if (value != null)
                {
                    foreach (DB.DBFileInfo file in value)
                        dataGridViewDB.Rows.Add(file.Id, file.path, file.size, file.date_);
                    numericUpDownDB.Maximum = value.Count;
                }
            }
        }

        public void UploadDGVDB(List<string[]> result)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Путь");
            dataTable.Columns.Add("Размер");
            dataTable.Columns.Add("Дата");

            foreach (string[] res in result)
            {
                dataTable.Rows.Add(res[0], res[1], res[2]);
            }
            dataGridViewDB.DataSource = dataTable;
        }


        public event EventHandler GetFileInputButtonEvent;
        public event EventHandler GetFileOutputButtonEvent;
        public event EventHandler GetGridRowClear;
        public event EventHandler GetGridRowAdd;
        public event EventHandler GetGridRowChange;
        public event EventHandler GetAnalyzeCode;
        public event EventHandler GetASMFuntion;

        public event EventHandler ConnectBDEvent;
        public event EventHandler DisconnectBDEvent;
        public event EventHandler GetDLLFileButtonEventDB;
        public event EventHandler ClearAllDLLFilesButtonEventDB;
        public event EventHandler DeleteDLLFileButtonEventDB;

        public string LoggerMess
        {
            set
            {
                richTextBoxLogger.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "] - " + value + '\n');
                richTextBoxLogger.ScrollToCaret();
            }
        }

        private void buttonDBConnect_Click(object sender, EventArgs e)
        {
            ConnectBDEvent?.Invoke(null, null);
            //enablesElementsDB();
        }

        private void buttonDBDiscocnnect_Click(object sender, EventArgs e)
        {

            DisconnectBDEvent?.Invoke(null, null);
            //enablesElementsDB();
        }

        private void buttonDBUpload_Click(object sender, EventArgs e)
        {
            GetDLLFileButtonEventDB?.Invoke(null, null);
        }

        private void buttonDBDeleteRow_Click(object sender, EventArgs e)
        {
            DeleteDLLFileButtonEventDB?.Invoke(null, null);
        }

        private void buttonDBDeleteAll_Click(object sender, EventArgs e)
        {
            ClearAllDLLFilesButtonEventDB?.Invoke(null, null);
        }





        public int indexDeleteRowDB { get => Int32.Parse(numericUpDownDB.Value.ToString()) - 1; }
        public int indexDeleteDB
        {
            set
            {
                dataGridViewDB.Rows.RemoveAt(value);
                numericUpDownDB.Maximum -= 1;
            }
        }
        public DB.DBFileInfo fileDB
        {
            set
            {
                if (value != null)
                {
                    dataGridViewDB.Rows.Add(value.Id, value.path, value.size, value.date_);
                    numericUpDownDB.Maximum += 1;
                }
            }
        }
        public int clearFilesDB
        {
            set
            {
                numericUpDownDB.Maximum = value;
                dataGridViewDB.Rows.Clear();
            }
        }

        public IList<DB.DBFileInfo> fileInDB
        {
            set
            {
                if (value != null)
                {
                    foreach (DB.DBFileInfo file in value)
                        dataGridViewDB.Rows.Add(file.Id, file.path, file.size, file.date_);
                    numericUpDownDB.Maximum = value.Count;
                }
            }
        }

        private void dataGridViewDB_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
