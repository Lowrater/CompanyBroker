using CompanyBroker.Interfaces;
using CompanyBroker.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
        private IDBService _iDBService;
        private IViewService _viewService;
        //---------------------------------- ICommands
        public ICommand CreateCommand => new RelayCommand<PasswordBox>(CreateAccount);

        //---------------------------------- Constructor
        public CreateAccountViewModel(IDBService __iDBService, IDataService __dataService, IAppConfigService __appConfigService, IViewService __viewService)
        {
            this._iDBService = __iDBService;
            this._dataService = __dataService;
            this._appConfigService = __appConfigService;
            this._viewService = __viewService;

            //-- Sets the companyList so the user can choose it.
            new Action(async () => CompanyList = await SetCompanylist())();
 
            //-- Sets the default value to true
            CompanyDropDownBool = true;
        }
        //---------------------------------- Properties

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
        /// Sets the companyList
        /// </summary>
        public async Task<ObservableCollection<string>> SetCompanylist()
        {
            using (var dbconnection = new SqlConnection(_appConfigService.SQL_connectionString))
            {
                return await _iDBService.RequestCompanyList(dbconnection, _appConfigService.SQL_FetchCompanyIdList, _appConfigService.MSG_CannotConnectToServer, true);
            }
        }

        /// <summary>
        /// Creates the account 
        /// </summary>
        public void CreateAccount(PasswordBox passwordBox)
        {
            using (var dbconnection = new SqlConnection(_appConfigService.SQL_connectionString))
            {
                if (NewCompanyBool.Equals(true))
                {
                    //-- Creates the company
                    _iDBService.CreateCompany(dbconnection, CompanyName, 0, NewCompanyBool, _appConfigService.MSG_FieldsCannotBeEmpty);
                    //-- Creates the account
                    _iDBService.CreateUser(dbconnection, CompanyChoice, AccountName, passwordBox.Password, AccountEmail, _appConfigService.MSG_FieldsCannotBeEmpty);
                    //-- Displays message
                    MessageBox.Show($"Account {AccountName} created for {CompanyName}!", "Company broker  message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    //-- Creates the account
                    _iDBService.CreateUser(dbconnection, CompanyChoice, AccountName, passwordBox.Password, AccountEmail, _appConfigService.MSG_FieldsCannotBeEmpty);
                    //-- Displays message
                    MessageBox.Show($"Account {AccountName} created!", "Company broker  message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                    //-- Closes the CreateAccountWindow window
                    _viewService.CloseWindow("CreateAccountWindow");
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
