using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ContentManagerMVC.WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPlayerService" in both code and config file together.
    [ServiceContract]
    public interface IPlayerService
    {
        [OperationContract]
        string DoWork();
        [OperationContract]
        string DownloadSchedule(int i);
        [OperationContract]
        int CheckScheduleCount();
        [OperationContract]
        DlFileBuff DownloadFile(DlFileInfo filename);
    }
    [DataContract]
    public class DlFileInfo
    {
        [DataMember]
        public string Name;
        [DataMember]
        public long StartPoint;
        [DataMember]
        public int Length;
    }
    [DataContract]
    public class DlFileBuff
    {
        [DataMember]
        public string Name;
        [DataMember]
        public long CurrPoint;
        [DataMember]
        public byte[] BufStream;
        [DataMember]
        public long FileSize;
    }  
    
}
