using CompanyBroker_API_Helper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker.Model
{
    public class BrokerOverviewModel
    {
        private string __searchField;
        public ref string _searchField => ref __searchField;




        private ObservableCollection<ResourcesModel> __mainRersourceList;
        public ref ObservableCollection<ResourcesModel> _mainRersourceList => ref __mainRersourceList;
    }
}
