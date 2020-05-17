using CompanyBroker.Model;
using CompanyBroker.View.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        CollectionListModel ListCollection { get; set; }
        bool isConnected { get; set; }

        string time { get; set; }
    }
}
