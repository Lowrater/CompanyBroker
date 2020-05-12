namespace CompanyBroker.Interfaces
{
    /// <summary>
    /// Holds all data written in the appconfig which will be used on different viewmodels.
    /// </summary>
    public interface IAppConfigService
    {

        //----------------------------------------- Messages
        string MSG_CannotConnectToServer { get; }
        string MSG_FieldsCannotBeEmpty { get; }
        string MSG_UknownUserName { get; }
    }
}