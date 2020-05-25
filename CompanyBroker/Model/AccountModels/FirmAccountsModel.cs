using CompanyBroker_API_Helper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker.Model.AccountModels
{
    public class FirmAccountsModel
    {
        private ObservableCollection<AccountModel> __mainAccountsList = new ObservableCollection<AccountModel>();
        public ref ObservableCollection<AccountModel> _mainAccountsList => ref __mainAccountsList;
    }
}
