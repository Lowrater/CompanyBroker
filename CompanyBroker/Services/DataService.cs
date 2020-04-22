using GalaSoft.MvvmLight;
using CompanyBroker.Interfaces;
using CompanyBroker.View.Windows;
using CompanyBroker.ViewModel;

namespace CompanyBroker.Services
{
    public class DataService : IDataService
    {

        public bool isConnected { get; set; }

        public string time { get; set; }

    }
}
