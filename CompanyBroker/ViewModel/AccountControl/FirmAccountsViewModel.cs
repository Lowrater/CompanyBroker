using CompanyBroker.Interfaces;
using CompanyBroker.Model.AccountModels;
using CompanyBroker.View.Windows;
using CompanyBroker.View.Windows.Informations;
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
using System.Windows;
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
        private IViewService _viewService;
        #endregion

        #region ICommands
        public ICommand AddAccountCommand => new RelayCommand(AddAccount);
        public ICommand RemoveAccountCommand => new RelayCommand<AccountModel>(async (a) => await RemoveAccount(a));
        public ICommand EditAccountCommand => new RelayCommand(EditAccount);
        public ICommand fetchUserAccountsCommand => new RelayCommand(async () => await FetchList());
        #endregion

        #region Constructor
        public FirmAccountsViewModel(IDataService __dataService, IViewService __viewService)
        {
            this._dataService = __dataService;
            this._viewService = __viewService;

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

        public AccountModel accountSelection
        {
            set => _dataService.accountSelection = value;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a new account to the system
        /// </summary>
        public void AddAccount()
        {
            _viewService.CreateWindow(new CreateAccountWindow());
        }

        /// <summary>
        /// Removes selected account
        /// </summary>
        /// <param name="accountModel"></param>
        public async Task RemoveAccount(AccountModel accountModel)
        {
            try
            {
                //-- Checks if the account is the logged in one
                if(accountModel.Username != _dataService.account.Username)
                {
                   if (MessageBox.Show("Are you sure you want to delete this account?", "CompanyBroker: account delete message",
                                       MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                        {
                        //-- trys to delete the account
                        var accountCheck = await new AccountProcessor().DeleteUserAccount(accountModel.CompanyId, accountModel.Username);

                        //-- checks if the account was deleted
                        if (accountCheck != false)
                        {
                            //-- Messages 
                            MessageBox.Show($"Successfully removed account: {accountModel.Username}",
                                            "Company Broker : remove account",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Information);
                        }
                        else
                        {
                            //-- Messages 
                            MessageBox.Show($"failed to remove account: {accountModel.Username}",
                                            "Company Broker : remove account",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Information);
                        }
                    }
                   
                }
            }
            catch (Exception e)
            {
                //-- Messages 
                MessageBox.Show(e.ToString().Substring(0, 252),
                                "CompanyBroker: resource error creation",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
            finally
            {
                await FetchList();
            }
        }

        /// <summary>
        /// Edits the selected accout
        /// </summary>
        public void EditAccount()
        {
            _viewService.CreateWindow(new AccountInfoWindow()); 
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
