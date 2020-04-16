using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CompanyBroker.Interfaces;
using CompanyBroker.Model;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Configuration;
using CompanyBroker.DbConnect;
using CompanyBroker.View.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace CompanyBroker.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        //------------------------------------------------------------------------------------------------ Models
        private LoginModel loginModel = new LoginModel();

        //------------------------------------------------------------------------------------------------ Interfaces
        /// <summary>
        /// For constructor injection for the Service
        /// </summary>
        private IDataService _dataService;
        private IViewService _viewService;
        private IAppConfigService _appConfigService;
        private IDBService _dBService;

        //------------------------------------------------------------------------------------------------ Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public LoginViewModel(IDataService dataService, IViewService viewService, IAppConfigService appConfigService, IDBService dBService)
        {
            this._dataService = dataService;
            this._viewService = viewService;
            this._appConfigService = appConfigService;
            this._dBService = dBService;
        }

        //------------------------------------------------------------------------------------------------ ICommands
        public ICommand LoginCommand => new RelayCommand<PasswordBox>(Login);
        public ICommand ExitCommand => new RelayCommand(Exit);

        //------------------------------------------------------------------------------------------------  Properties
        /// <summary>
        /// UserName for login screen
        /// Stores the value in loginModel and in the DataService MsSQLUserInfo
        /// </summary>
        public string UserName
        {
            get => loginModel._username;
            set
            {
                Set(ref loginModel._username, value);
            }
        }


        //------------------------------------------------------------------------------------------------ Methods
        /// <summary>
        /// Login function, which takes the Password field as parameter, and verifys login informations
        /// </summary>
        /// <param name="password"></param>
        private void Login(PasswordBox password)
        {
            //-- Verifys if the userName is empty or blank
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(password.Password))
            {
                //-- Trying loggin in the user account and returns an MsSqlUserInfo
                _dataService.msSQLUserInfo = _dBService.ConnectToServer(password, UserName, _appConfigService.MSG_CannotConnectToServer);

                if (_dataService.msSQLUserInfo.IsConnected != false)
                {
                    //-- Messages the user that they are logged in
                    MessageBox.Show("Logged in!",
                                    "Company Broker",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);

                    //-- Opens MainWindow via. new viewService interface
                    _viewService.CreateWindow(new MainWindow());

                    //-- Hides LoginWindow
                    foreach (Window window in Application.Current.Windows)
                    {
                        //-- Searches for a window with the following LoginWindow to remove it so the user can use the MainWindow of the application
                        if (window.Title.Equals("LoginWindow"))
                        {
                            window.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"{_appConfigService.MSG_UknownUserName}",
                                   "Company Broker login error",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show($"{_appConfigService.MSG_FieldsCannotBeEmpty}",
                          "Company Broker login error",
                         MessageBoxButton.OK,
                         MessageBoxImage.Error);             
            }    
        }

        /// <summary>
        /// Closes the application
        /// </summary>
        private void Exit()
        {
            Application.Current.Shutdown();
        }



    }
}