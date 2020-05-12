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
        //---------------------------------- Models
        private CreateAccountModel createAccountModel = new CreateAccountModel();
        //---------------------------------- Interfaces
        private IAppConfigService _appConfigService;
        private IDataService _dataService;
        private IViewService _viewService;

        //---------------------------------- ICommands
        public ICommand CreateCommand => new RelayCommand<PasswordBox>(async (PasswordBox) => await CreateAccount(PasswordBox.Password));

        //---------------------------------- Constructor
        public CreateAccountViewModel(IDataService __dataService, IAppConfigService __appConfigService, IViewService __viewService)
        {
            this._dataService = __dataService;
            this._appConfigService = __appConfigService;
            this._viewService = __viewService;

            //-- Sets the companyList so the user can choose it.
            new Action(async () => CompanyList = await SetCompanylist())();
 
            //-- Sets the default value to true
            CompanyDropDownBool = true;
        }
        //---------------------------------- Properties

        private bool accountCreations { get; set; }
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

        //---------------------------------- Methods
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
                //-- Add an empty to have the correct choosing of index
                companyList.Add("");

                //-- loops through the list and adds only the name of the data.
                foreach (CompanyModel company in list)
                {
                    companyList.Add(company.Name);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString().Substring(0,252), "Company broker message", MessageBoxButton.OK, MessageBoxImage.Error);
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
            bool httpResultCreationResponse = false;

            //-- Creates the account
            var account = new CreateAccountAPIModel
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
                        httpResultCreationResponse = await new CompanyProcesser().CreateCompany(company);

                        //-- Fetches the company created by companyName
                        var companyObject = await new CompanyProcesser().GetCompany(company.CompanyName);
                        //-- sets the account companyID
                        account.CompanyId = companyObject.Id;

                        //-- Sends the account for creation
                        httpResultCreationResponse = await new AccountProcessor().CreateAccount(account);

                        //-- checks wheter or not it was successfull
                        if (httpResultCreationResponse == true)
                        {
                            //-- Displays message
                            MessageBox.Show($"Account {AccountName} created for {CompanyName}!", "Company broker  message", MessageBoxButton.OK, MessageBoxImage.Error);

                            //-- Closes the CreateAccountWindow window
                            _viewService.CloseWindow("CreateAccountWindow");
                        }
                        else
                        {
                            //-- Displays message
                            MessageBox.Show($"Account {AccountName} and {CompanyName} not created! Please verify your connection, or contact the support team", "Company broker  message", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Company name must be filled", "Company broker  message", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    //-- Sends the account
                    httpResultCreationResponse = await new AccountProcessor().CreateAccount(account);

                    if (httpResultCreationResponse == true)
                    {
                        //-- Displays message
                        MessageBox.Show($"Account {AccountName} created!", "Company broker  message", MessageBoxButton.OK, MessageBoxImage.Error);

                        //-- Closes the CreateAccountWindow window
                        _viewService.CloseWindow("CreateAccountWindow");
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
    }
}
