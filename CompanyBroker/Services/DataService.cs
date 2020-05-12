using GalaSoft.MvvmLight;
using CompanyBroker.Interfaces;
using CompanyBroker.View.Windows;
using CompanyBroker.ViewModel;

namespace CompanyBroker.Services
{
    /// <summary>
    /// Data service class that shares data with every viewmodel
    /// </summary>
    public class DataService : IDataService
    {
        public bool isConnected { get; set; }

        public string time { get; set; }

    }
}
