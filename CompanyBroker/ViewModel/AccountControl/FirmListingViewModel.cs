using CompanyBroker.Interfaces;
using CompanyBroker.Model.AccountModels;
using CompanyBroker.View.Windows;
using CompanyBroker.View.Windows.Informations;
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
        public ICommand EditListingCommand => new RelayCommand<ResourcesModel>((r) => EditListing(r));
        public ICommand RefreshListingCommand => new RelayCommand(async () => await FetchList());
        public ICommand RemoveListingCommand => new RelayCommand<ResourcesModel>(async (r) => await DeleteProduct(r));
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


        /// <summary>
        /// Deletes selectedItem from the datagrid binding
        /// </summary>
        /// <param name="resourcesModel"></param>
        /// <returns></returns>
        public async Task DeleteProduct(ResourcesModel resourcesModel)
        {
            try
            {
                //-- Fetches the resource selected
                var resourceCheck = await new ResourcesProcesser().GetResourceByCompanyIdAndName(resourcesModel.CompanyId, resourcesModel.ProductName);
                //-- Checks the resource
                if(resourceCheck != null)
                {
                    if (MessageBox.Show("Are you sure you want to delete this product?", "CompanyBroker: resource delete message",
                                       MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        var isReourceDeleted = await new ResourcesProcesser().DeleteProduct(resourcesModel.CompanyId, resourcesModel.ProductName, resourcesModel.ResourceId);

                        if (isReourceDeleted != false)
                        {
                            //-- Resource deleted
                        }
                        else
                        {
                            //-- Messages 
                            MessageBox.Show("Resource could not be deleted",
                                            "CompanyBroker: resource delete error",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Information);
                        }
                    }
                }
                else
                {
                    //-- Messages 
                    MessageBox.Show("Resource could not be deleted",
                                    "CompanyBroker: resource delete error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }

            }
            catch (Exception e)
            {
                //-- Messages 
                MessageBox.Show(e.ToString().Substring(0, 252),
                                "CompanyBroker: resource delete error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
            finally
            {
                await FetchList();
            }
        }


        /// <summary>
        /// Opens window of EditResourceInfoWindow for listing
        /// </summary>
        /// <param name="resourcesModel"></param>
        /// <returns></returns>
        public void EditListing(ResourcesModel resourcesModel)
        {
            //-- Sets the resourceSelection
            _dataService.ResourceSelection = resourcesModel;
            //-- Opens the resource edit window
            _viewService.CreateWindow(new EditResourceInfoWindow());
        }

        #endregion
    }
}
