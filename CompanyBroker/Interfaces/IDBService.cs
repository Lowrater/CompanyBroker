using CompanyBroker.DbConnect;
using System.Windows.Controls;

namespace CompanyBroker.Interfaces
{
    public interface IDBService
    {
        MsSQLUserInfo ConnectToServer(PasswordBox password, string UserName, string SQL_VerifyUserName, string MSG_CannotConnectToServer);
    }
}