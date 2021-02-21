using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFProject_CE091_CE081
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEncDecService" in both code and config file together.
    [ServiceContract]
    public interface IEncDecService
    {
        [OperationContract]
        string EncryptData(string key, string data);

        [OperationContract]
        string DecryptData(string key, string data);
    }
}
