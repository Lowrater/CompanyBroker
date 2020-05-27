using CompanyBroker.Interfaces;
using CompanyBroker.Model.AccountModels;
using CompanyBroker_API_Helper;
using CompanyBroker_API_Helper.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyBroker.ViewModel.AccountControl
{

    public class FirmAccountsViewModel : ViewModelBase
    {
        #region Models     
        private FirmAccountsModel firmListingModel = new FirmAccountsModel();
        #endregion

        #region Interfaces    
        private IDataService _dataService;
        #endregion

        #region ICommands
        public ICommand AddAccountCommand => new RelayCommand(AddAccount);
        #endregion

        #region Constructor
        public FirmAccountsViewModel(IDataService __dataService)
        {
            this._dataService = __dataService;

            //-- Sets the list content
            new Action(async () => await FetchList())();
        }
        #endregion

        #region Properties      
        public ObservableCollection<AccountModel> MainAccountsList
        {
            get => firmListingModel._mainAccountsList;
            set => Set(ref firmListingModel._mainAccountsList, value);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a new account to the system
        /// </summary>
        public void AddAccount()
        {

        }

        /// <summary>
        /// Sets the content list based on the logged in account and it's company ID value
        /// </summary>
        /// <returns></returns>
        public async Task FetchList()
        {
            MainAccountsList = await new AccountProcessor().FetchAccountsByCompanyId(_dataService.account.CompanyId);
        }

        #endregion

    }
}
