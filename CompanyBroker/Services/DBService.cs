using CompanyBroker.DbConnect;
using CompanyBroker.Interfaces;
using System;
using System.Configuration;
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
        public MsSQLUserInfo ConnectToServer(PasswordBox password, string UserName, string SQL_VerifyUserName, string MSG_CannotConnectToServer)
        {
            MsSQLUserInfo msSQLUserInfo = new MsSQLUserInfo();
            msSQLUserInfo.DBuserName = "";
            msSQLUserInfo.IsConnected = false;

            //-- internal variable to store the SQL result of the username
            string loginResult;
            //-- Gets the connectionstring from app.Config of InteractDBS tag.
            var appcConnectionString = ConfigurationManager.ConnectionStrings["InteractDBS"].ConnectionString;

            //-- sets up the sqlconnection
            using (SqlConnection connection = new SqlConnection(appcConnectionString))
            {
                //-- sets up the sqlcommand and executing
                using (SqlCommand newQueryCommand = new SqlCommand($"{SQL_VerifyUserName} '{UserName}'", connection))
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
    }
}
