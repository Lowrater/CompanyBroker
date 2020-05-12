using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CompanyBroker.Interfaces;
using CompanyBroker.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyBroker.View.Windows;
using NUnit.Framework;
using System.Threading.Tasks;
using CompanyBroker_API_Helper;
using CompanyBroker_API_Helper.Models;

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

        //------------------------------------------------------------------------------------------------ Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public LoginViewModel(IDataService dataService, IViewService viewService, IAppConfigService appConfigService)
        {
            this._dataService = dataService;
            this._viewService = viewService;
            this._appConfigService = appConfigService;
        }

        //------------------------------------------------------------------------------------------------ ICommands
        public ICommand LoginCommand => new RelayCommand<PasswordBox>(async (PasswordBox) => await Login(PasswordBox.Password));
        public ICommand ExitCommand => new RelayCommand(Exit);
        public ICommand CreateCommand => new RelayCommand(CreateAccount);

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
        private async Task Login(string password)
        {
            //-- Verifys if the userName is empty or blank
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(password))
            {
                try
                {
                    //-- creates the new model of an login request
                    var account = new LoginAPIModel
                    {
                        Username = UserName,
                        Password = password
                    };

                    //-- Verifys login 
                    bool result = await new AccountProcessor().VerifyAccount(account);

                    //-- verifys the results
                    if(result != false)
                    {
                        //-- Messages the user that they are logged in
                        MessageBox.Show("Logged in!",
                                        "Company Broker",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);

                        //-- Sets the active state to true
                        _dataService.isConnected = true;
                        //-- Opens MainWindow via. new viewService interface
                        _viewService.CreateWindow(new MainWindow());
                        //-- Closes LoginWindow
                        _viewService.CloseWindow("LoginWindow");
                    }
                    else
                    {
                        MessageBox.Show($"{_appConfigService.MSG_UknownUserName}",
                                        "Company broker Server error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                    }
                }
                catch (Exception exception)
                {

                    //-- checks the exception type
                    if (exception is AssertionException)
                    {
                        MessageBox.Show($"{_appConfigService.MSG_UknownUserName}",
                                        "Company broker Server error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                    }
                    else
                    {
                        //-- prints out software exception message
                        MessageBox.Show($"{exception.Message}",
                                        "Company broker Server error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                    }
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

        /// <summary>
        /// Creates the 'Create account window' for the user
        /// </summary>
        public void CreateAccount()
        {
            //-- Opens CreateAccountWindow via. new viewService interface
            _viewService.CreateWindow(new CreateAccountWindow());
        }

    }
}