using CompanyBroker.Interfaces;
using CompanyBroker.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        //---------------------------------- ICommands
        public ICommand CreateCommand => new RelayCommand(CreateAccount);

        //---------------------------------- Constructor
        public CreateAccountViewModel(IDBService __iDBService, IDataService __dataService, IAppConfigService __appConfigService)
        {
            this._iDBService = __iDBService;
            this._dataService = __dataService;
            this._appConfigService = __appConfigService;

            //-- Sets the companyList so the user can choose it.
            SetCompanylist();
        }
        //---------------------------------- Properties

        /// <summary>
        /// Sets the states wheter or not the user creates a new business to the system or creates an account to an existing company
        /// </summary>
        public bool NewCompanyBool
        {
            get => createAccountModel._newCompanyBool;
            set => Set(ref createAccountModel._newCompanyBool, value);
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

        public string AccountPassword
        {
            get => createAccountModel._accountPassword;
            set => Set(ref createAccountModel._accountPassword, value);
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
        public void SetCompanylist()
        {
            using (var dbconnection = new SqlConnection(_appConfigService.SQL_connectionString))
            {
                CompanyList = _iDBService?.RequestCompanyList(dbconnection, _appConfigService.SQL_FetchCompanyIdList, _appConfigService.MSG_CannotConnectToServer);
            }
        }

        /// <summary>
        /// Creates the account 
        /// </summary>
        public void CreateAccount()
        {

            if(NewCompanyBool.Equals(true))
            {
                using (var dbconnection = new SqlConnection(_appConfigService.SQL_connectionString))
                {
                    //-- Creates the company
                    _iDBService.CreateCompany(dbconnection, CompanyName, 0, NewCompanyBool);
                    //-- Creates the account
                    _iDBService.CreateUser(dbconnection, CompanyChoice, AccountName, AccountPassword, AccountEmail);
                }
            }
            else
            {
                //-- Creates the account
                using (var dbconnection = new SqlConnection(_appConfigService.SQL_connectionString))
                {
                    _iDBService.CreateUser(dbconnection, CompanyChoice, AccountName, AccountPassword, AccountEmail);
                }
            }
        }
    }
}
