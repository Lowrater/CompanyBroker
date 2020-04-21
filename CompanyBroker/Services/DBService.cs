using CompanyBroker.DbConnect;
using CompanyBroker.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        /// Random method for generating Salt for the password agens the DBS
        /// Is created here because the 'new instancing' of the random property, creating the same number on each runtime is possible.
        /// Is from education course
        /// </summary>
        private readonly Random random = new Random();

        /// <summary>
        /// Method to generate password salt for the DBS. Needs an number. Typical 32.
        /// Is from education course
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private byte[] GenerateSalt(int size)
        {
            var salt = new byte[size];
            random.NextBytes(salt);
            return salt;
        }

        /// <summary>
        /// Creates an HASH out of the password the user has entered.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private  byte[] GetHash(string s, byte[] salt)
        {
            using (var ha = HashAlgorithm.Create("SHA256"))
                return ha.ComputeHash(salt.Concat(Encoding.UTF8.GetBytes(s)).ToArray());
        }

        /// <summary>
        /// Creates an user to the system
        /// Is from education course
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="companyId"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void CreateUser(IDbConnection dbConnection, int companyId, string username, string password, string Email)
        {
            var salt = GenerateSalt(32);
            var passwordHash = GetHash(password, salt);

            using (var cmd = dbConnection.CreateCommand())
            {
                //-- The command text
                cmd.CommandText = "INSERT INTO CompanyAccounts (CompanyId, Username, Email, PasswordHash, PasswordSalt, Active) " +
                                  "VALUES (@CompanyId, @Username, @Email, @PasswordHash, @PasswordSalt, @Active);";

                //-- All command parameters
                var companyIdParam = cmd.CreateParameter();
                companyIdParam.ParameterName = "@CompanyId";
                companyIdParam.Value = companyId;

                var usernameParam = cmd.CreateParameter();
                usernameParam.ParameterName = "@Username";
                usernameParam.Value = username;

                var emailParam = cmd.CreateParameter();
                emailParam.ParameterName = "@Email";
                emailParam.Value = Email;

                var passwordHashParam = cmd.CreateParameter();
                passwordHashParam.ParameterName = "@PasswordHash";
                passwordHashParam.Value = passwordHash;
                passwordHashParam.DbType = DbType.Binary;
                passwordHashParam.Size = 32;

                var passwordSaltParam = cmd.CreateParameter();
                passwordSaltParam.ParameterName = "@PasswordSalt";
                passwordSaltParam.Value = salt;
                passwordHashParam.DbType = DbType.Binary;
                passwordHashParam.Size = 32;

                var activeParam = cmd.CreateParameter();
                activeParam.ParameterName = "@Active";
                activeParam.Value = true;

                //-- Adding the parameters
                cmd.Parameters.Add(companyIdParam);
                cmd.Parameters.Add(usernameParam);
                cmd.Parameters.Add(emailParam);
                cmd.Parameters.Add(passwordHashParam);
                cmd.Parameters.Add(passwordSaltParam);
                cmd.Parameters.Add(activeParam);

                //-- Opening the connectiong
                if (dbConnection.State != ConnectionState.Open)
                    dbConnection.Open();

                //-- Executes the query with no return
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Creates an user to the system
        /// Is from education course
        /// </summary>
        /// <param name="dbConnection"></param>
        public void CreateCompany(IDbConnection dbConnection, string companyName, int balance, bool active)
        {
            using (var cmd = dbConnection.CreateCommand())
            {
                //-- The command text
                cmd.CommandText = "INSERT INTO Companies (CompanyName, CompanyBalance, Active) " +
                                  "VALUES (@CompanyName, @CompanyBalance, @Active);";

                //-- All command parameters
                var companyNameParam = cmd.CreateParameter();
                companyNameParam.ParameterName = "@CompanyName";
                companyNameParam.Value = companyName;

                var companyBalanceParam = cmd.CreateParameter();
                companyBalanceParam.ParameterName = "@CompanyBalance";
                companyBalanceParam.Value = balance;
                companyBalanceParam.DbType = DbType.Int32;

                var activeParam = cmd.CreateParameter();
                activeParam.ParameterName = "@Active";
                activeParam.Value = active;

                //-- Adding the parameters
                cmd.Parameters.Add(companyNameParam);
                cmd.Parameters.Add(companyBalanceParam);
                cmd.Parameters.Add(activeParam);

                //-- Opening the connectiong
                if (dbConnection.State != ConnectionState.Open)
                    dbConnection.Open();

                //-- Executes the query with no return
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Validates the user, it's password and hash/salt
        /// Is from education course
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool VerifyLogin(IDbConnection dbConnection, string username, string password)
        {
            using (var cmd = dbConnection.CreateCommand())
            {
                cmd.CommandText = "SELECT PasswordHash, PasswordSalt " +
                                  "FROM CompanyAccounts " +
                                  "WHERE Username = @Username";

                var usernameParam = cmd.CreateParameter();
                usernameParam.ParameterName = "@Username";
                usernameParam.Value = username;

                cmd.Parameters.Add(usernameParam);

                if (dbConnection.State != ConnectionState.Open)
                    dbConnection.Open();

                using (var rdr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    while (rdr.Read())
                    {
                        var passwordHashOrdinal = rdr.GetOrdinal("PasswordHash");
                        var passwordSaltOrdinal = rdr.GetOrdinal("PasswordSalt");

                        var passwordHash = rdr.IsDBNull(passwordHashOrdinal) ? null : new byte[rdr.GetBytes(passwordHashOrdinal, 0, null, 0, 0)];
                        rdr.GetBytes(passwordHashOrdinal, 0, passwordHash, 0, passwordHash.Length);

                        var passwordSalt = rdr.IsDBNull(passwordSaltOrdinal) ? null : new byte[rdr.GetBytes(passwordSaltOrdinal, 0, null, 0, 0)];
                        rdr.GetBytes(passwordSaltOrdinal, 0, passwordSalt, 0, passwordSalt.Length);

                        return GetHash(password, passwordSalt).SequenceEqual(passwordHash);
                    }
                }
            }

            return false;
        }


        /// <summary>
        /// Connects to the database, and fetches all the companies from the Companies table into an ObservableCollection<string>
        /// </summary>
        /// <param name="msSQLUserInfo"></param>
        /// <param name="fetchCompanyListCommand"></param>
        /// <param name="MSG_CannotConnectToServer"></param>
        /// <returns></returns>
        public ObservableCollection<string> RequestCompanyList(IDbConnection dbConnection, string fetchCompanyListCommand, string MSG_CannotConnectToServer)
        {
            //-- The content holder
            var companyList = new ObservableCollection<string>();

            try
            {
                //-- sets up the sqlcommand and executing
                using (var SQLCommand = dbConnection.CreateCommand())
                {
                    //-- Creates the SQL command
                    SQLCommand.CommandText = fetchCompanyListCommand;

                    //-- opens the connections
                    //-- connection != null && connection.State != ConnectionState.Open
                    //-- connection?.State != ConnectionState.Open
                    if (dbConnection != null && dbConnection?.State != ConnectionState.Open)
                        {
                            //-- Opens the connection
                            dbConnection?.Open();
                            //-- Executes the command
                            using (var reader = SQLCommand?.ExecuteReader())
                            {
                            //-- Fetches the content
                                while (reader.Read())
                                {
                                //-- Adds the content to the list
                                    companyList.Add(reader?.GetString(0));
                                }
                            }
                        }
                        else
                        {
                            //-- Executes the command
                            using (var reader = SQLCommand?.ExecuteReader())
                            {
                                //-- Fetches the content
                                while (reader.Read())
                                {
                                    //-- Adds the content to the list
                                    companyList.Add(reader?.GetString(0));
                                }
                            }
                        }
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
            //-- returns the companyList
            return companyList;
        }


        /// <summary>
        /// Connects to the database, and fetches all the resources from the CompanyResources table into an ObservableCollection<string>
        /// </summary>
        /// <param name="msSQLUserInfo"></param>
        /// <param name="fetchResourceListCommand"></param>
        /// <param name="MSG_CannotConnectToServer"></param>
        /// <returns></returns>
        public ObservableCollection<string> RequestProductTypeList(IDbConnection dbConnection, string SQL_ProductTypeList, string MSG_CannotConnectToServer)
        {
            //-- The content holder
            var resourceList = new ObservableCollection<string>();

            try
            {
                //-- sets up the sqlcommand and executing
                using (var SQLCommand = dbConnection.CreateCommand())
                {
                    //-- Creates the SQL command
                    SQLCommand.CommandText = SQL_ProductTypeList;

                    //-- opens the connections
                    //-- connection != null && connection.State != ConnectionState.Open
                    //-- connection?.State != ConnectionState.Open
                    if (dbConnection != null && dbConnection?.State != ConnectionState.Open)
                    {
                        //-- Opens the connection
                        dbConnection?.Open();

                        //-- Executes the command
                        using (var reader = SQLCommand?.ExecuteReader())
                        {
                            //-- Fetches the content
                            while (reader.Read())
                            {
                                //-- Adds the content to the list
                                resourceList.Add(reader?.GetString(0));
                            }
                        }
                    }
                    else
                    {
                        //-- Executes the command
                        using (var reader = SQLCommand?.ExecuteReader())
                        {
                            //-- Fetches the content
                            while (reader.Read())
                            {
                                //-- Adds the content to the list
                                resourceList.Add(reader?.GetString(0));
                            }
                        }
                    }
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


            //-- returns the resourceList
            return resourceList;
        }
    }
}
