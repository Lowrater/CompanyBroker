using CompanyBroker.DbConnect;
using CompanyBroker.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace CompanyBroker.Services
{
    /// <summary>
    /// DBSService handles all the SQL queries and data as return types for other classes.
    /// </summary>
    public class DBService : IDBService
    {

        /// <summary>
        /// Method to try to login on the server and returns an MsSQLUserInfo
        /// </summary>
        /// <param name="password"></param>
        /// <param name="UserName"></param>
        /// <param name="SQL_VerifyUserName"></param>
        /// <param name="MSG_CannotConnectToServer"></param>
        /// <returns></returns>
        public MsSQLUserInfo ConnectToServer(PasswordBox password, string UserName, string MSG_CannotConnectToServer)
        {
            MsSQLUserInfo msSQLUserInfo = new MsSQLUserInfo();

            //-- internal variable to store the SQL result of the username
            string loginResult;
            //-- Gets the connectionstring from app.Config of InteractDBS tag.
            var appcConnectionString = ConfigurationManager.ConnectionStrings["CompanyDBS"].ConnectionString;

            //-- sets up the sqlconnection
            using (SqlConnection connection = new SqlConnection(appcConnectionString))
            {
                //-- sets up the sqlcommand and executing
                using (SqlCommand newQueryCommand = new SqlCommand($"Select Username from CompanyAccounts where Username= '{UserName}' and Userpassword = '{password.Password}'", connection))
                {
                    try
                    {
                        //-- opens the connections
                        connection.Open();

                        //-- reads the gets the return value from the SQL server to verify on
                        loginResult = (string)newQueryCommand.ExecuteScalar();

                        //-- Checks if the username exist
                        //-- checks if the username is the same as the input 
                        if (!string.IsNullOrEmpty(loginResult) && UserName == loginResult.Trim())
                        {
                            //-- Connection opened, saves the connectionstring in the global dataservices.
                            msSQLUserInfo.DBuserName = UserName;
                            msSQLUserInfo.IsConnected = true;
                            msSQLUserInfo.sqlconnection = connection;
                        }
                    }
                    catch (Exception exception)
                    {
                        //-- checks the exception type
                        if (exception is SqlException)
                        {
                            MessageBox.Show($"{MSG_CannotConnectToServer}",
                                            "Interact Server error",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error);

                        }
                        else
                        {
                            //-- prints out software exception message
                            MessageBox.Show($"{exception.Message.Substring(0, 250)}",
                                            "Interact Server error",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error);
                        }
                    }
                }
            }


            //-- Returns the informations depending on the login
            return msSQLUserInfo;
        }


        /// <summary>
        /// Connects to the database, and fetches all the companies from the Companies table into an ObservableCollection<string>
        /// </summary>
        /// <param name="msSQLUserInfo"></param>
        /// <param name="fetchCompanyListCommand"></param>
        /// <param name="MSG_CannotConnectToServer"></param>
        /// <returns></returns>
        public ObservableCollection<string> RequestCompanyList(MsSQLUserInfo msSQLUserInfo, string fetchCompanyListCommand, string MSG_CannotConnectToServer)
        {
            //-- The content holder
            ObservableCollection<string> companyList = new ObservableCollection<string>();

            //-- sets up the sqlconnection
            using (msSQLUserInfo.sqlconnection)
            {
                //-- sets up the sqlcommand and executing
                using (SqlCommand newQueryCommand = new SqlCommand(fetchCompanyListCommand, msSQLUserInfo.sqlconnection))
                {
                    try
                    {
                        //-- opens the connections
                        if(msSQLUserInfo.sqlconnection != null && msSQLUserInfo.sqlconnection.State != ConnectionState.Open)
                        {
                            msSQLUserInfo.sqlconnection.Open();

                            //-- Executes the sql command

                            //-- Adds the company names to the list

                        }
                        else
                        {
                            //-- Executes the sql command

                            //-- Adds the company names to the list
                        }
                    }
                    catch (Exception exception)
                    {
                        //-- checks the exception type
                        if (exception is SqlException)
                        {
                            MessageBox.Show($"{MSG_CannotConnectToServer}",
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
            }


            //-- returns the companyList
            return companyList;
        }
    }
}
