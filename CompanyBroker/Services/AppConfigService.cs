using System.Configuration;
using CompanyBroker.Interfaces;

namespace CompanyBroker.Services
{
    /// <summary>
    /// Holds all data written in the appconfig which will be used on different viewmodels.
    /// </summary>
    public class AppConfigService : IAppConfigService
    {
        //----------------------- SQL
        public string SQL_FetchCompanyList => ConfigurationManager.AppSettings["SQL_FetchCompanyList"];

        //----------------------- Messages
        public string MSG_UknownUserName => ConfigurationManager.AppSettings["LoginWindow_UknownUserNameMSG"];
        public string MSG_CannotConnectToServer => ConfigurationManager.AppSettings["LoginWindow_CannotConnectToServerMSG"];
        public string MSG_FieldsCannotBeEmpty => ConfigurationManager.AppSettings["LoginWindow_FieldsCannotBeEmptyMSG"];

        //----------------------- Headers
    }
}
