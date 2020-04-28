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
        public string SQL_connectionString => ConfigurationManager.ConnectionStrings["CompanyDBS"].ConnectionString;
        public string SQL_FetchCompanyList => ConfigurationManager.AppSettings["SQL_FetchCompanyList"];
        public string SQL_FetchCompanyIdList => ConfigurationManager.AppSettings["SQL_FetchCompanyIdList"];
        public string SQL_ProductTypeList => ConfigurationManager.AppSettings["SQL_ProductTypeList"];
        public string SQL_FetchAllResources => ConfigurationManager.AppSettings["SQL_FetchAllResources"];

        public string SQL_FetchSpecificProductNames => ConfigurationManager.AppSettings["SQL_FetchSpecificProductNames"];

        //----------------------- Messages
        public string MSG_UknownUserName => ConfigurationManager.AppSettings["LoginWindow_UknownUserNameMSG"];
        public string MSG_CannotConnectToServer => ConfigurationManager.AppSettings["LoginWindow_CannotConnectToServerMSG"];
        public string MSG_FieldsCannotBeEmpty => ConfigurationManager.AppSettings["AllFields_FieldsCannotBeEmptyMSG"];

        //----------------------- Headers
    }
}
