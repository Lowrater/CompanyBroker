using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using InteractWPF.Interfaces;
using InteractWPF.Model;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Configuration;
using InteractWPF.DbConnect;
using InteractWPF.View.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace InteractWPF.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        //------------------------------------------------------------------------------------------------ Models
        LoginModel loginModel = new LoginModel();

        //------------------------------------------------------------------------------------------------ Variables
        /// <summary>
        /// For dependency injection for the DataService
        /// </summary>
        private IDataService _dataService;
        private IViewService _viewService;

        /// <summary>
        /// Constructor
        /// </summary>
        public LoginViewModel(IDataService dataService, IViewService viewService)
        {
            this._dataService = dataService;
            this._viewService = viewService;
        }

        //------------------------------------------------------------------------------------------------ ICommands
        public ICommand LoginCommand => new RelayCommand<PasswordBox>(Login);
        public ICommand ExitCommand => new RelayCommand(Exit);

        //------------------------------------------------------------------------------------------------ Functions
        /// <summary>
        /// Login function, which takes the Password field as parameter, and verifys login informations
        /// </summary>
        /// <param name="password"></param>
        private void Login(PasswordBox password)
        {
            //-- internal variable to store the SQL result of the username
            string loginResult;
            //-- Gets the connectionstring from app.Config of InteractDBS tag.
            var appcConnectionString = ConfigurationManager.ConnectionStrings["InteractDBS"].ConnectionString;

            //-- Verifys if the userName is empty or blank
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(password.Password))
            {
                //-- sets up the sqlconnection
                using (SqlConnection connection = new SqlConnection(appcConnectionString))
                {
                    //-- sets up the sqlcommand and executing
                    using (SqlCommand newQueryCommand = new SqlCommand($"{ConfigurationManager.AppSettings["SQL_VerifyUserName"]} '{UserName}'", connection))
                    {
                        try
                        {
                            //-- opens the connections
                            connection.Open();

                            //-- reads the gets the return value from the SQL server to verify on
                            loginResult = (string)newQueryCommand.ExecuteScalar();

                            //-- Checks if the username exist
                            //-- checks if the username is the same as the input 
                            if(!string.IsNullOrEmpty(loginResult) && this.UserName == loginResult.Trim())
                            {
                                //-- Connection opened, saves the connectionstring in the global dataservices.
                                _dataService.msSQLUserInfo = new MsSQLUserInfo
                                {
                                    DBuserName = this.UserName,
                                    IsConnected = true
                                };

                                //-- Messages the user that they are logged in
                                MessageBox.Show("Logged in!",
                                                "Logged in message",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Information);

                                //-- Opens MainWindow via. new viewService interface
                                _viewService.CreateWindow(new MainWindow());

                                //-- Hides LoginWindow
                                Application.Current.MainWindow.Hide();
                            }
                            else
                            {
                                MessageBox.Show($"{ConfigurationManager.AppSettings["LoginWindow_UknownUserNameMSG"]}",
                                               "Interact login error",
                                              MessageBoxButton.OK,
                                              MessageBoxImage.Error);
                            }
                        }
                        catch (Exception exception)
                        {    
                            //-- checks the exception type
                            if(exception is SqlException)
                            {
                                MessageBox.Show($"{ConfigurationManager.AppSettings["LoginWindow_CannotConnectToServerMSG"]}",
                                                "Interact Server error",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Error);
                            }
                            else
                            {
                                //-- prints out software exception message
                                MessageBox.Show($"{exception.Message.Substring(0,250)}",
                                                "Interact Server error",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Error);
                            }                        
                        }                      
                    }
                }
            }
            else
            {
                MessageBox.Show($"{ConfigurationManager.AppSettings["LoginWindow_FieldsCannotBeEmptyMSG"]}",
                          "Interact login error",
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

        //------------------------------------------------------------------------------------------------ Element Bindings
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

    }
}