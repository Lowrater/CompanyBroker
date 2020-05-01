using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CompanyBroker.Interfaces
{
    public interface IDBService
    {
        bool VerifyLogin(IDbConnection dbConnection, string username, string password);
        void CreateUser(IDbConnection dbConnection, int companyId, string username, string password, string Email, string MSG_FieldsCannotBeEmpty);
        void CreateCompany(IDbConnection dbConnection, string companyName, int balance, bool active, string MSG_FieldsCannotBeEmpty);

        Task<ObservableCollection<string>> RequestCompanyList(IDbConnection dbConnection, string fetchCompanyListCommand, string MSG_CannotConnectToServer, bool withId);
        Task<ObservableCollection<string>> RequestDBSList(IDbConnection dbConnection, string SQL_ProductTypeList, string MSG_CannotConnectToServer);
        Task<ObservableCollection<string>> RequestDBSList(IDbConnection dbConnection, string SQL_Command, string ParameterValue, string MSG_CannotConnectToServer);

        Task<DataTable> ExecuteQuery(IDbConnection dbConnection, string QueryCommand);
    }
}