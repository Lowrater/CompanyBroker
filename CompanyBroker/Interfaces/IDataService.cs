using CompanyBroker.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker.Interfaces
{
    /// <summary>
    /// Data service which contains all usable data which is needed to be shared or set by between viewmodels.
    /// </summary>
   public interface IDataService
    {
        bool isConnected { get; set; }

        string time { get; set; }
    }
}
