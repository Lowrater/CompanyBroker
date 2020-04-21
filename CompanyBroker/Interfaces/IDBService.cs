using CompanyBroker.DbConnect;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Controls;

namespace CompanyBroker.Interfaces
{
    public interface IDBService
    {
        bool VerifyLogin(IDbConnection dbConnection, string username, string password);
        void CreateUser(IDbConnection dbConnection, int companyId, string username, string password, string Email);
        void CreateCompany(IDbConnection dbConnection, string companyName, int balance, bool active);

        ObservableCollection<string> RequestCompanyList(IDbConnection dbConnection, string fetchCompanyListCommand, string MSG_CannotConnectToServer);

        ObservableCollection<string> RequestProductTypeList(IDbConnection dbConnection, string SQL_ProductTypeList, string MSG_CannotConnectToServer);
    }
}