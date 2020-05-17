using GalaSoft.MvvmLight;
using CompanyBroker.Interfaces;
using CompanyBroker.View.Windows;
using CompanyBroker.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CompanyBroker.Model;

namespace CompanyBroker.Services
{
    /// <summary>
    /// Data service class that shares data with every viewmodel
    /// </summary>
    public class DataService : IDataService
    {
        public CollectionListModel ListCollection { get; set; }
        public bool isConnected { get; set; }

        public string time { get; set; }

    }
}
