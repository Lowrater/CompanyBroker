using CompanyBroker_API_Helper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker.Model.AccountModels
{
    public class FirmListingModel
    {
        private ObservableCollection<ResourcesModel> __mainListingList = new ObservableCollection<ResourcesModel>();
        public ref ObservableCollection<ResourcesModel> _mainListingList => ref __mainListingList;
    }
}
