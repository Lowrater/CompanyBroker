using CompanyBroker.DbConnect;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace CompanyBroker.Interfaces
{
    public interface IDBService
    {
        MsSQLUserInfo ConnectToServer(PasswordBox passwordBox, string UserName, string MSG_CannotConnectToServer, string SQL_connectionString);

        ObservableCollection<string> RequestCompanyList(MsSQLUserInfo msSQLUserInfo, string fetchCompanyListCommand, string MSG_CannotConnectToServer);

        ObservableCollection<string> RequestProductTypeList(MsSQLUserInfo msSQLUserInfo, string SQL_ProductTypeList, string MSG_CannotConnectToServer);
    }
}