using DllAnd;
using FileWorker;
using SP_AndreevLab1._1.logger;
using SP_AndreevLab1._1.model;
using SP_AndreevLab1._1.view;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_AndreevLab1._1.presenter
{
    internal class Presenter
    {
        private IView _view;
        private IModel _model;
        private ILogger _logger;

        public Presenter(IView view)
        {
            _model = new Model();
            _logger = new Logger();
            _view = view;

            _view.GetFileInputButtonEvent += new EventHandler(FileIn);
            _view.GetFileOutputButtonEvent += new EventHandler(buttonFileOutput_Click);
            _view.GetGridRowClear += new EventHandler(buttonGridRowClear_Click);
            _view.GetGridRowAdd += new EventHandler(buttonGridRowAdd_Click);
            _view.GetGridRowChange += new EventHandler(buttonGridRowChange_Click);
            _view.GetAnalyzeCode += new EventHandler(buttonDoAnalyze_Click);
            _view.GetASMFuntion += new EventHandler(Model);

            _view.ConnectBDEvent += new EventHandler(ConnectDB);
            _view.DisconnectBDEvent += new EventHandler(DisconnectDB);
            _view.GetDLLFileButtonEventDB += new EventHandler(GetDLLFileDB);
            _view.ClearAllDLLFilesButtonEventDB += new EventHandler(ClearDLLFilesDB);
            _view.DeleteDLLFileButtonEventDB += new EventHandler(DeleteDLLFileDB);
        }

        private void LoggerMess(string message) => _view.LoggerMess = message;

        private void FileIn(object sender, EventArgs e) {
            try {
                LoggerMess("Начало загрузки файла");
                List<string[]> files = _model.FileIn();
                _view.ListResult = files;
                LoggerMess("Файл успешно загружен");
            } 
            catch (Exception ex) {
                string result = ex.Message;
                LoggerMess($"Непредвиденная ошибка: {ex.Message}");
                _logger.Show(result);
            }
            
        }

        private void buttonFileOutput_Click(object sender, EventArgs e)
        {
            try {
                LoggerMess("Файл сохраняется");
                _model.FileOut();
                LoggerMess("Файл успешно сохранён");    
            } 
            catch (Exception ex){
                string result = ex.Message;
                LoggerMess($"Непредвиденная ошибка: {ex.Message}");
                _logger.Show(result);
            }
        }

        private void buttonGridRowClear_Click(object sender, EventArgs e)
        {
            try { 
                _view.ListResult = _model.ClearSelectedRow(_view.GetID);
                LoggerMess("Удалена строка");
            } 
            catch (Exception ex) {
                string result = ex.Message;
                LoggerMess($"Непредвиденная ошибка: {ex.Message}");
                _logger.Show(result);
            }   
        }

        private void buttonGridRowAdd_Click(object sender, EventArgs e)
        {
            try { 
                _view.ListResult = _model.AddRow(_view.DataText);
                LoggerMess("Дообавлена строка");
            } 
            catch (Exception ex) {
                string result = ex.Message;
                LoggerMess($"Непредвиденная ошибка: {ex.Message}");
                _logger.Show(result);
            }
        }
        private void buttonGridRowChange_Click(object sender, EventArgs e)
        {
            try { 
                _view.ListResult = _model.UpgradeRow(_view.GetID, _view.DataText);
                LoggerMess("Обновление строки");
            } 
            catch (Exception ex) {
                string result = ex.Message;
                LoggerMess($"Непредвиденная ошибка: {ex.Message}");
                _logger.Show(result);
            }
        }
        private void buttonDoAnalyze_Click(object sender, EventArgs e)
        {
            try {
                LoggerMess("Начало анализа кода");
                
                string result = _model.AnalyzerDoWhile(_view.Cycle);
                if (Int32.TryParse(result, out int res)) {
                    _view.AnalyzerRes1 = result;
                    LoggerMess("Анализ кода успешно завершён");
                }
                else {
                    _view.AnalyzerRes1 = "-";
                    LoggerMess("Анализ кода завершён. В коде ошибка: " + result);
                }
                
            } 
            catch (Exception ex) {
                string result = ex.Message;
                LoggerMess($"Непредвиденная ошибка: {ex.Message}");
                _logger.Show(result);
            }
        }
        public void Model(object sender, EventArgs e)
        {
            try {
                LoggerMess("Начало умножения низкоуровневой вствкой");
                _view.ASMRes = _model.ASMAnd(_view.value1, _view.value2);
                LoggerMess("Умножение низкоуровневой вставкой завершено");
            } 
            catch (Exception ex) {
                string result = ex.Message;
                LoggerMess($"Непредвиденная ошибка: {ex.Message}");
                _logger.Show(result);
            }
        }





        private void ConnectDB(object sender, EventArgs e)
        {
            try
            {
                LoggerMess("Подключение к базе данных...");
                _view.fileInDB = _model.connectBD();
                LoggerMess("Подключение к базе данных завершено");
            }
            catch (Exception ex)
            {
                string result = ex.Message;
                LoggerMess($"Непредвиденная ошибка: {ex.Message}");
                _logger.Show(result);
            }
        }

        private void DisconnectDB(object sender, EventArgs e)
        {
            try
            {
                LoggerMess("Отключение от базы данных...");
                _view.clearFilesDB = _model.disconnectBD();
                LoggerMess("Отключение от базы данных завершено");
            }
            catch (Exception ex)
            {
                string result = ex.Message;
                LoggerMess($"Непредвиденная ошибка: {ex.Message}");
                _logger.Show(result);
            }
        }

        private void GetDLLFileDB(object sender, EventArgs e)
        {
            try
            {
                List<DB.DBFileInfo> result = _model.AddDllFileDB();
                if (result != null)
                {
                    LoggerMess("Начало загрузки файла");
                    
                    _view.ListResultDB = result
                        ; 
                    LoggerMess("Файл успешно загружен");
                }
            }
            catch (Exception ex)
            {

                string result = ex.Message;
                LoggerMess($"Непредвиденная ошибка: {ex.Message}");
                _logger.Show(result);
            }
        }

        private void ClearDLLFilesDB(object sender, EventArgs e)
        {
            try
            {
                int result = _model.clearAllDllFilesDB();
                _view.clearFilesDB = 0;
                LoggerMess("Список DLL файлов из базы данных очищен, удалено файлов " + result);
            }
            catch (Exception ex)
            {
                string result = ex.Message;
                LoggerMess($"Непредвиденная ошибка: {ex.Message}");
                _logger.Show(result);
            }
        }

        private void DeleteDLLFileDB(object sender, EventArgs e)
        {
            try
            {
                int index = _view.indexDeleteRowDB;
                if (index != -1)
                {
                    string result = _model.deleteDllFileDB(index);
                    _view.indexDeleteDB = index;
                    LoggerMess("DLL файл " + result + " удален из базы данных");
                }
            }
            catch (Exception ex)
            {
                string result = ex.Message;
                LoggerMess($"Непредвиденная ошибка: {ex.Message}");
                _logger.Show(result);
            }
        }

    }
}
