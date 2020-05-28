using CompanyBroker.Interfaces;
using CompanyBroker.Model.AccountModels;
using CompanyBroker.View.Windows;
using CompanyBroker_API_Helper;
using CompanyBroker_API_Helper.Models;
using CompanyBroker_API_Helper.Processers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CompanyBroker.ViewModel.AccountControl
{
    public class FirmListingViewModel : ViewModelBase
    {

        #region Models     
        private FirmListingModel firmListingModel = new FirmListingModel();
        #endregion

        #region Interfaces
        private IDataService _dataService;
        private IViewService _viewService;
        #endregion

        #region ICommands
        public ICommand AddListingCommand => new RelayCommand(AddListing);
        public ICommand RefreshListingCommand => new RelayCommand(async () => await FetchList());
        #endregion

        #region Constructor
        public FirmListingViewModel(IDataService __dataService, IViewService __viewService)
        {
            this._dataService = __dataService;
            this._viewService = __viewService;

            //-- Sets the list content
            new Action(async () => await FetchList())();
        }
        #endregion

        #region Properties
        public ObservableCollection<ResourcesModel> MainListingList
        {
            get => firmListingModel._mainListingList;
            set => Set(ref firmListingModel._mainListingList, value);
        }
        #endregion


        #region Methods
        /// <summary>
        /// Opens the CompanyAddListingWindow
        /// </summary>
        public void AddListing()
        {
            _viewService.CreateWindow(new CompanyAddListingWindow());
        }

        /// <summary>
        /// Sets the content list based on the logged in account and it's company ID value
        /// </summary>
        /// <returns></returns>
        public async Task FetchList()
        {
            try
            {
                MainListingList = await new ResourcesProcesser().GetAllResourcesByCompanyId(_dataService.account.CompanyId);
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
