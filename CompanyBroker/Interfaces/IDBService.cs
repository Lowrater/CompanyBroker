using CompanyBroker.DbConnect;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace CompanyBroker.Interfaces
{
    public interface IDBService
    {
        MsSQLUserInfo ConnectToServer(PasswordBox password, string UserName, string MSG_CannotConnectToServer);

        ObservableCollection<string> RequestCompanyList(MsSQLUserInfo msSQLUserInfo, string fetchCompanyListCommand, string MSG_CannotConnectToServer);
    }
}