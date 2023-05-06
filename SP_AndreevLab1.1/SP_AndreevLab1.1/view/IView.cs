using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_AndreevLab1._1.view
{
    internal interface IView
    {
        event EventHandler GetFileInputButtonEvent;
        event EventHandler GetFileOutputButtonEvent;
        event EventHandler GetGridRowClear;
        event EventHandler GetGridRowAdd;
        event EventHandler GetGridRowChange;
        event EventHandler GetAnalyzeCode;
        event EventHandler GetASMFuntion;

        event EventHandler ConnectBDEvent;
        event EventHandler DisconnectBDEvent;
        event EventHandler GetDLLFileButtonEventDB;
        event EventHandler ClearAllDLLFilesButtonEventDB;
        event EventHandler DeleteDLLFileButtonEventDB;

        string LoggerMess { set; }
        string Cycle { get; }
        string AnalyzerRes1 { set; }
        int value1 { get; }
        int value2 { get; }
        string ASMRes { set; }
        string[] DataText { get; }
        int GetID { get; }

        List<string[]> ListResult { set; }



        List<DB.DBFileInfo> ListResultDB { set; }
        int indexDeleteRowDB { get; }
        int indexDeleteDB { set; }
        DB.DBFileInfo fileDB { set; }
        int clearFilesDB { set; }
        IList<DB.DBFileInfo> fileInDB { set; }
    }
}
