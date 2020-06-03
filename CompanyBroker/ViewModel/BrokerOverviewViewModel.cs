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
        public ICommand BuyResourceCommand => new RelayCommand<ResourcesModel>(async (rm) => await BuyResource(rm));
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
        /// Selection of an row type of ResourceModel
        /// </summary>
        public ResourcesModel ResourceRowSelection
        {
            get => brokerOverviewModel._resourceSelection;
            set
            {
                Set(ref brokerOverviewModel._resourceSelection, value);
                //-- Sets the value in the dataService for ResourceInfoViewModel to display
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

        /// <summary>
        /// Method to buy selected resource
        /// </summary>
        /// <param name="selectedSellerResource"></param>
        /// <returns></returns>
        public async Task BuyResource(ResourcesModel selectedSellerResource)
        {
            try
            { 
                if(selectedSellerResource.CompanyId != _dataservice.account.CompanyId)
                {
                    //-- fetches the resource selected for the logged in account company
                    var buyerResourceCheck = await new ResourcesProcesser().GetResourceByCompanyIdAndName(_dataservice.account.CompanyId, selectedSellerResource.ProductName);

                    //-- If we don't have the resource, then make a copy of it, and blank it's informations
                    if (buyerResourceCheck == null)
                    {
                        //-- Copys the selected resource
                        var NewResource = selectedSellerResource;
                        //-- changes the informations
                        NewResource.CompanyId = _dataservice.account.CompanyId;
                        NewResource.Active = false;
                        NewResource.Amount = 1;
                        NewResource.ResourceId = new int();

                        //-- Changes the balance
                        var balanceCheck = await ChangeCompanyBalance(selectedSellerResource, _dataservice.account.CompanyId);

                        if (balanceCheck != false)
                        {
                            //-- Adds the resource to the database
                            var boughtResource = await new ResourcesProcesser().AddNewResources(NewResource);

                            if (boughtResource != false)
                            {
                                //- decreases the for the resource count
                                var ChangedresourceAmount = await new ResourcesProcesser().ChangeCompanyResourceAmount(selectedSellerResource.CompanyId, selectedSellerResource.ResourceId, false);
                            }
                            else
                            {
                                //-- Messages 
                                MessageBox.Show($"Could not purchase {selectedSellerResource.ProductName}",
                                                "CompanyBroker: resource error creation",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Information);
                            }
                        }
                    }
                    else if (buyerResourceCheck != null)
                    {
                        //-- If we have it, increase the amount, and degrease the selected company resource of this resource
                        //-- Decrease the companyBalance of the purcher
                        //-- Increase the companyBalance of the seller
                        var balanceCheck = await ChangeCompanyBalance(selectedSellerResource, _dataservice.account.CompanyId);

                        if (balanceCheck != false)
                        {
                              // Changes the resource amounts based on seller and buyer
                            var ChangedresourceSeller = await new ResourcesProcesser().ChangeCompanyResourceAmount(selectedSellerResource.CompanyId, selectedSellerResource.ResourceId, false);
                            var ChangedresourceBuyer = await new ResourcesProcesser().ChangeCompanyResourceAmount(_dataservice.account.CompanyId, buyerResourceCheck.ResourceId, true);

                            if(ChangedresourceSeller != ChangedresourceBuyer)
                            {
                                //-- Messages 
                                MessageBox.Show($"Error in balance checks",
                                                "CompanyBroker: resource error purchase",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Information);
                            }
                        }
                    }
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
            finally
            {
                //-- Update the table
                await FillTable();
            }
        }

        /// <summary>
        /// Changes the company Balance
        /// </summary>
        /// <returns></returns>
        private async Task<bool> ChangeCompanyBalance(ResourcesModel sellerResourcesModel, int buyerCompanyId)
        {
            try
            {
                //-- Decrease the companyBalance of the purcher
                var DecreaseBuyerBalance = await new CompanyProcesser().ChangeCompanyBalance(buyerCompanyId, false, sellerResourcesModel.Price);

                //-- Verifys if the user has the money and can buy the resource
                if(DecreaseBuyerBalance != false)
                {
                    //-- Increase the companyBalance of the seller
                    var IncreaseSellerBalance = await new CompanyProcesser().ChangeCompanyBalance(sellerResourcesModel.CompanyId, true, sellerResourcesModel.Price);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                //-- Messages 
                MessageBox.Show(e.ToString().Substring(0, 252),
                                "CompanyBroker: resource error creation",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                return false;
            }
        }






        #endregion

    }

}
