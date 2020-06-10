using System.Configuration;
using CompanyBroker.Interfaces;

namespace CompanyBroker.Services
{
    /// <summary>
    /// Holds all data written in the appconfig which will be used on different viewmodels.
    /// </summary>
    public class AppConfigService : IAppConfigService
    {
        //----------------------- Messages
        public string MSG_UknownUserName => ConfigurationManager.AppSettings["LoginWindow_UknownUserNameMSG"];
        public string MSG_CannotConnectToServer => ConfigurationManager.AppSettings["LoginWindow_CannotConnectToServerMSG"];
        public string MSG_FieldsCannotBeEmpty => ConfigurationManager.AppSettings["AllFields_FieldsCannotBeEmptyMSG"];

        public string MSG_AccountIsInActive => ConfigurationManager.AppSettings["MSG_AccountIsInActive"];


        //----------------------- Headers
    }
}
