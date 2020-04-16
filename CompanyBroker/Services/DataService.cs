using GalaSoft.MvvmLight;
using CompanyBroker.DbConnect;
using CompanyBroker.Interfaces;
using CompanyBroker.View.Windows;
using CompanyBroker.ViewModel;

namespace CompanyBroker.Services
{
    public class DataService : IDataService
    {

        public MsSQLUserInfo msSQLUserInfo { get; set; }

        public string time { get; set; }

    }
}
