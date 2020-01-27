using System.Configuration;
using InteractWPF.Interfaces;

namespace InteractWPF.Services
{
    /// <summary>
    /// Holds all data written in the appconfig which will be used on different viewmodels.
    /// </summary>
    public class AppConfigService : IAppConfigService
    {
        //----------------------- SQL
        public string SQL_VerifyUserName => ConfigurationManager.AppSettings["SQL_VerifyUserName"];

        //----------------------- Messages
        public string MSG_UknownUserName => ConfigurationManager.AppSettings["LoginWindow_UknownUserNameMSG"];
        public string MSG_CannotConnectToServer => ConfigurationManager.AppSettings["LoginWindow_CannotConnectToServerMSG"];
        public string MSG_FieldsCannotBeEmpty => ConfigurationManager.AppSettings["LoginWindow_FieldsCannotBeEmptyMSG"];

        //----------------------- Headers
    }
}
