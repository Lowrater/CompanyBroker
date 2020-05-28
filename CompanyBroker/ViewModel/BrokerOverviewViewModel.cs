using CompanyBroker.Interfaces;
using CompanyBroker.Model;
using CompanyBroker.View.Windows.Informations;
using CompanyBroker_API_Helper.Models;
using CompanyBroker_API_Helper.Processers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CompanyBroker.ViewModel
{
    /// <summary>
    /// Used for BrokerOverviewControl, SearchBarControl
    /// Uses filters from SidePanelTab1
    /// </summary>
    /// 
    public class BrokerOverviewViewModel : ViewModelBase
    {
        #region Models
        private BrokerOverviewModel brokerOverviewModel = new BrokerOverviewModel();
        #endregion

        #region Interfaces
        private IDataService _dataservice;
        private IViewService _viewservice;

        #endregion

        #region ICommands
        public ICommand ExecuteQueryCommand => new RelayCommand(async () => await FillTable());
        public ICommand OpenResourceInfoWindowCommand => new RelayCommand(OpenResourceInfoWindow);
        #endregion

        #region Constructor
        public BrokerOverviewViewModel(IDataService __dataservice, IViewService __viewservice)
        {
            this._dataservice = __dataservice;
            this._viewservice = __viewservice;
        }
        #endregion

        #region Properties

        /// <summary>
        /// The main list (table) which contains all the resources
        /// </summary>
        public ObservableCollection<ResourcesModel> MainRersourceList
        {
                get => brokerOverviewModel._mainRersourceList;
                set => Set(ref brokerOverviewModel._mainRersourceList, value);
            }

        /// <summary>
        /// Search field with text
        /// </summary>
        public string SearchField
        {
            get => brokerOverviewModel._searchField;
            set
            {
                Set(ref brokerOverviewModel._searchField, value);
            }
        }

        /// <summary>
        /// Selection of an row
        /// </summary>
        public ResourcesModel ResourceSelection
        {
            get => brokerOverviewModel._resourceSelection;
            set
            {
                Set(ref brokerOverviewModel._resourceSelection, value);
                _dataservice.ResourceSelection = value;
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Opens a new resource info window, on the selected resource
        /// </summary>
        public void OpenResourceInfoWindow()
        {
            _viewservice.CreateWindow(new ResourceInfoWindow());
        }

        /// <summary>
        /// Fills the table for the user, depending on the filters provided from the user in the SidePanelTab1ViewModel
        /// </summary>
        public async Task FillTable()
        {
            try
            {
                if (_dataservice.FilterCollection == null && string.IsNullOrEmpty(SearchField))
                {
                    MainRersourceList = await new ResourcesProcesser().GetAllResources();
                }
                else if (_dataservice.FilterCollection != null && string.IsNullOrEmpty(SearchField))
                {
                    MainRersourceList = await new ResourcesProcesser().GetResourcesByListFilters(_dataservice.FilterCollection);
                }
                else if (_dataservice.FilterCollection != null && !string.IsNullOrEmpty(SearchField))
                {
                    //-- Sets the searchWord
                    _dataservice.FilterCollection.SearchWord = SearchField;
                    MainRersourceList = await new ResourcesProcesser().GetResourcesByListFilters(_dataservice.FilterCollection);
                    _dataservice.FilterCollection.SearchWord = string.Empty;
                }
                else if (_dataservice.FilterCollection == null && !string.IsNullOrEmpty(SearchField))
                {
                    MainRersourceList = await new ResourcesProcesser().GetResourcesBySearch(SearchField);
                }
            }
            catch (Exception e)
            {
                //-- Messages 
                MessageBox.Show(e.ToString().Substring(0, 252),
                                "CompanyBroker: resource error creation",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }

        }
        #endregion

    }

}
