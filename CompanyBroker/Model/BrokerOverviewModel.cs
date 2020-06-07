using CompanyBroker_API_Helper.Models;
using System.Collections.ObjectModel;

namespace CompanyBroker.Model
{
    public class BrokerOverviewModel
    {
        private string __searchField;
        public ref string _searchField => ref __searchField;

        private ObservableCollection<ResourcesModel> __mainRersourceList = new ObservableCollection<ResourcesModel>();
        public ref ObservableCollection<ResourcesModel> _mainRersourceList => ref __mainRersourceList;
    }
}
