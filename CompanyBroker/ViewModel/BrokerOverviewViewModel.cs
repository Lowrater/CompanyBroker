using CompanyBroker.Addons.Extensions;
using CompanyBroker.Interfaces;
using CompanyBroker.Model;
using CompanyBroker_API_Helper.Models;
using CompanyBroker_API_Helper.Processers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyBroker.ViewModel
{
    /// <summary>
    /// Used for BrokerOverviewControl, SearchBarControl
    /// Uses filters from SidePanelTab1
    /// </summary>
    public class BrokerOverviewViewModel : ViewModelBase
    {
        //---------------------------------------------------------------- Model
        private BrokerOverviewModel brokerOverviewModel = new BrokerOverviewModel();

        //---------------------------------------------------------------- Interfaces
        private IDataService _dataservice;
        private IAppConfigService _appConfigService;
        private IContentService _contentService;

        //---------------------------------------------------------------- ICommands
        public ICommand ExecuteQueryCommand => new RelayCommand(async () => await FillDataTable());

        //---------------------------------------------------------------- Constructor
        public BrokerOverviewViewModel(IDataService __dataservice, IAppConfigService __appConfigService, IContentService __contentService)
        {
            this._dataservice = __dataservice;
            this._appConfigService = __appConfigService;
            this._contentService = __contentService;
        }

        //---------------------------------------------------------------- Properties
        /// <summary>
        /// The main list (table) which contains all the resources
        /// </summary>
        public ObservableCollection<ResourcesModel> MainRersourceList
        {
                get => brokerOverviewModel._mainRersourceList;
                set => Set(ref brokerOverviewModel._mainRersourceList, value);
            }

        public string SearchField
        {
            get => brokerOverviewModel._searchField;
            set => Set(ref brokerOverviewModel._searchField, value);
        }

        //---------------------------------------------------------------- Methods
        /// <summary>
        /// Fills the table for the user, depending on the filters provided from the user in the SidePanelTab1ViewModel
        /// </summary>
        public async Task FillDataTable()
        {
            //-- Checks if the listcollection is empty and the search field.
            if (_dataservice.ListCollection == null && string.IsNullOrEmpty(SearchField))
            {
                //-- Fetches all resources without filter
                MainRersourceList = await new ResourcesProcesser().GetAllResources();

            }

        }

    }
}
