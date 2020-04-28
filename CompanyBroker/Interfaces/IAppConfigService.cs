namespace CompanyBroker.Interfaces
{
    /// <summary>
    /// Holds all data written in the appconfig which will be used on different viewmodels.
    /// </summary>
    public interface IAppConfigService
    {
        //----------------------------------------- SQL commands
        string SQL_connectionString { get; }
        string SQL_ProductTypeList { get; }
        string SQL_FetchCompanyList { get; }
        string SQL_FetchCompanyIdList { get; }
        string SQL_FetchAllResources { get; }

        string SQL_FetchSpecificProductNames { get; }

        //----------------------------------------- Messages
        string MSG_CannotConnectToServer { get; }
        string MSG_FieldsCannotBeEmpty { get; }
        string MSG_UknownUserName { get; }
    }
}