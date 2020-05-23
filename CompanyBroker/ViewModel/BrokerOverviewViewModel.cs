using CompanyBroker.Interfaces;
using CompanyBroker.Model;
using CompanyBroker_API_Helper.Models;
using CompanyBroker_API_Helper.Processers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
        public ICommand ExecuteQueryCommand => new RelayCommand(async () => await FillTable());

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
            set
            {
                Set(ref brokerOverviewModel._searchField, value);
            }
        }

        //---------------------------------------------------------------- Methods
        /// <summary>
        /// Fills the table for the user, depending on the filters provided from the user in the SidePanelTab1ViewModel
        /// </summary>
        public async Task FillTable()
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
    }
    
}
