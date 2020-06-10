using CompanyBroker.Interfaces;
using CompanyBroker.Model;
using CompanyBroker_API_Helper;
using CompanyBroker_API_Helper.Models;
using CompanyBroker_API_Helper.Processers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyBroker.ViewModel
{
    public class CreateAccountViewModel : ViewModelBase
    {
        #region Models
        private CreateAccountModel createAccountModel = new CreateAccountModel();
        #endregion

        #region Interfaces
        private IAppConfigService _appConfigService;
        private IDataService _dataService;
        private IViewService _viewService;
        #endregion

        #region ICommands
        public ICommand CreateCommand => new RelayCommand<PasswordBox>(async (PasswordBox) => await CreateAccount(PasswordBox.Password));
        #endregion

        #region Constructor
        public CreateAccountViewModel(IDataService __dataService, IAppConfigService __appConfigService, IViewService __viewService)
        {
            this._dataService = __dataService;
            this._appConfigService = __appConfigService;
            this._viewService = __viewService;

            //-- Sets the companyList so the user can choose it.
            new Action(async () => CompanyList = await SetCompanylist())();
 
            //-- Sets the default value to true
            CompanyDropDownBool = true;

            //-- if we are connected, use the current account company details
            if(_dataService.isConnected == true)
            {
                CompanyDropDownBool = false;
                CompanyNameBool = false;
                IsLoggedIn = false;
            }
            else
            {
                IsLoggedIn = true;
            }

        }
        #endregion

        #region Properties
        /// <summary>
        /// Sets the states wheter or not the user creates a new business to the system or creates an account to an existing company
        /// </summary>
        public bool NewCompanyBool
        {
            get => createAccountModel._newCompanyBool;
            set
            {
                Set(ref createAccountModel._newCompanyBool, value);
                //-- Sets the state of CompanyDropDownBool
                SetCompanyListSate(value);
            }
        }

        public bool IsLoggedIn
        {
            get => createAccountModel._isLoggedIn;
            set => Set(ref createAccountModel._isLoggedIn, value);
        }
        public bool CompanyDropDownBool
        {
            get => createAccountModel._companyDropDownBool;
            set => Set(ref createAccountModel._companyDropDownBool, value);
        }

        public bool CompanyNameBool
        {
            get => createAccountModel._companyNameBool;
            set => Set(ref createAccountModel._companyNameBool, value);
        }

        public string CompanyName
        {
            get => createAccountModel._companyName;
            set => Set(ref createAccountModel._companyName, value);
        }

        public string AccountName
        {
            get => createAccountModel._accountName;
            set => Set(ref createAccountModel._accountName, value);
        }

        public int CompanyChoice
        {
            get => createAccountModel._companyChoice;
            set => Set(ref createAccountModel._companyChoice, value);
        }

        public string AccountEmail
        {
            get => createAccountModel._accountEmail;
            set => Set(ref createAccountModel._accountEmail, value);
        }

        public ObservableCollection<string> CompanyList
        {
            get => createAccountModel._companyList;
            set => Set(ref createAccountModel._companyList, value);
        }

        #endregion


        #region Methods

        /// <summary>
        /// Sets the companyList through the WebAPI
        /// </summary>
        public async Task<ObservableCollection<string>> SetCompanylist()
        {
            //-- Creates a new list with strings
            var companyList = new ObservableCollection<string>();

            try
            {
                //-- Fetches all the company data
                var list = await new CompanyProcesser().GetAllCompanies();
                //-- loops through the list and adds only the name of the data.
                foreach (CompanyModel company in list)
                {
                    companyList.Add(company.Name);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString().Substring(0, 252), "Company broker message", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //-- returns the list
            return companyList;
        }

        /// <summary>
        /// Creates the account 
        /// </summary>
        public async Task CreateAccount(string password)
        {
            //-- Result response of the request to create an account to the database
            bool accountCheck = false;

            //-- Creates the account
            var account = new AccountAPIModel
            {
                CompanyId = CompanyChoice,
                Username = AccountName,
                Email = AccountEmail,
                Password = password,
                Active = true
            };

            try
            {              
                if (NewCompanyBool.Equals(true))
                {
                    if(!string.IsNullOrEmpty(CompanyName))
                    {
                        //-- Creates the company
                        var company = new CreateCompanyAPIModel
                        {
                            CompanyName = CompanyName
                        };

                        //-- sends the company for creation
                        accountCheck = await new CompanyProcesser().CreateCompany(company);

                        //-- Fetches the company created by companyName
                        var companyObject = await new CompanyProcesser().GetCompany(company.CompanyName);
                        //-- sets the account companyID
                        account.CompanyId = companyObject.Id;

                        //-- Sends the account for creation
                        accountCheck = await new AccountProcessor().CreateAccount(account);

                        //-- checks wheter or not it was successfull
                        if (accountCheck == true)
                        {
                            //-- Displays message
                            MessageBox.Show($"Account {AccountName} created for {CompanyName}!", "Company broker  message", MessageBoxButton.OK, MessageBoxImage.Information);

                            //-- Closes the CreateAccountWindow window
                            _viewService.CloseWindow("TheCreateAccountWindow");
                        }
                        else
                        {
                            //-- Displays message
                            MessageBox.Show($"Account {AccountName} and {CompanyName} not created! Please verify your connection, or contact the support team", "Company broker  message", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Company name must be filled", "Company broker  message", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    //-- Sets the current companyId if an account is logged in to create an account for an company
                    if(_dataService.isConnected == true)
                    {
                        account.CompanyId = _dataService.account.CompanyId;
                    }

                    //-- Sends the account
                    accountCheck = await new AccountProcessor().CreateAccount(account);

                    if (accountCheck == true)
                    {
                        //-- Displays message
                        MessageBox.Show($"Account {AccountName} created!", "Company broker  message", MessageBoxButton.OK, MessageBoxImage.Information);

                        //-- Closes the CreateAccountWindow window
                        _viewService.CloseWindow("TheCreateAccountWindow");
                    }
                    else
                    {
                        //-- Displays message
                        MessageBox.Show($"Account {AccountName} not created! Please verify your connection, or contact the support team", "Company broker  message", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString().Substring(252), "Company broker  message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Sets the companyListState inactive or active depending if the account need a new company
        /// </summary>
        /// <param name="NewCompanyBool"></param>
        public void SetCompanyListSate(bool NewCompanyBool)
        {
            if(NewCompanyBool.Equals(true))
            {
                CompanyDropDownBool = false;
                CompanyNameBool = true;
            }
            else
            {
                CompanyDropDownBool = true;
                CompanyNameBool = false;
            }
        }

        #endregion
    }
}
