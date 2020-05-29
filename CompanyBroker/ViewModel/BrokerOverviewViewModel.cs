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
        public ICommand BuyResourceCommand => new RelayCommand(async () => await BuyResource());
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
        public async Task BuyResource()
        {
            try
            { 
                if(ResourceSelection.CompanyId != _dataservice.account.CompanyId)
                {
                    //-- fetches the resource selected for the logged in account company
                    var resourceCheck = await new ResourcesProcesser().GetResourceByCompanyIdAndName(_dataservice.account.CompanyId, ResourceSelection.ProductName);

                    //-- If we don't have the resource, then make a copy of it, and blank it's informations
                    if (resourceCheck == null)
                    {
                        //-- Copys the selected resource
                        var resource = this.ResourceSelection;
                        //-- changes the informations
                        resource.CompanyId = _dataservice.account.CompanyId;
                        resource.Active = false;
                        resource.Amount = 1;
                        resource.ResourceId = new int();

                        //-- Changes the balance
                        var balanceCheck = await ChangeCompanyBalance();

                        if (balanceCheck != false)
                        {
                            //-- Adds the resource to the database
                            var boughtResource = await new ResourcesProcesser().AddNewResources(resource);

                            if (boughtResource != false)
                            {
                                //- New amountChangeModel
                                var changeResourced = new ResourceAmountChangeModel
                                {
                                    companyResource = ResourceSelection,
                                    increase = false
                                };

                                //- decreases the for the resource count
                                var ChangedresourceAmount = await new ResourcesProcesser().ChangeCompanyResourceAmount(changeResourced);

                                if (ChangedresourceAmount != false)
                                {
                                    //-- Update the table
                                    await FillTable();
                                }
                            }
                            else
                            {
                                //-- Messages 
                                MessageBox.Show($"Could not purchase {resource.ProductName}",
                                                "CompanyBroker: resource error creation",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Information);
                            }
                        }
                    }
                    else if (resourceCheck != null)
                    {
                        //-- If we have it, increase the amount, and degrease the selected company resource of this resource
                        //-- Decrease the companyBalance of the purcher
                        //-- Increase the companyBalance of the seller
                        var balanceCheck = await ChangeCompanyBalance();

                        if (balanceCheck != false)
                        {
                            //-- Decrease the company resource amount of the seller
                            //- decreases the for the resource count
                            var ChangedresourceAmount = await ChangeCompanyResourceAmount(resourceCheck, ResourceSelection);

                            if (ChangedresourceAmount != false)
                            {
                                //-- Update the table
                                await FillTable();
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
        }

        /// <summary>
        /// Changes the company Balance
        /// </summary>
        /// <returns></returns>
        private async Task<bool> ChangeCompanyBalance()
        {
            //-- Object of balance model. Decrease the values of the buyer
            var deCreaseBuyer = new CompanyBalanceModel
            {
                companyId = _dataservice.account.CompanyId,
                increase = false,
                priceAmount = ResourceSelection.Price
            };

            //- Object of balance model. Incease the values of the seller
            var inCreaseSeller = new CompanyBalanceModel
            {
                companyId = ResourceSelection.CompanyId,
                increase = true,
                priceAmount = ResourceSelection.Price
            };

            try
            {
                //-- Decrease the companyBalance of the purcher
                var DecreaseBuyerBalance = await new CompanyProcesser().ChangeCompanyBalance(deCreaseBuyer);

                //-- Verifys if the user has the money and can buy the resource
                if(DecreaseBuyerBalance != false)
                {
                    //-- Increase the companyBalance of the seller
                    var IncreaseSellerBalance = await new CompanyProcesser().ChangeCompanyBalance(inCreaseSeller);
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



        /// <summary>
        /// Changes the resource amounts based on seller and buyer
        /// </summary>
        /// <param name="BuyerResource"></param>
        /// <param name="SellerResource"></param>
        /// <returns></returns>
        private async Task<bool> ChangeCompanyResourceAmount(ResourcesModel BuyerResource, ResourcesModel SellerResource)
        {
            try
            {
                if (BuyerResource != null && SellerResource != null)
                {
                    //-- Decrease the company resource amount of the buyer
                    var changeSeller = new ResourceAmountChangeModel
                    {
                        companyResource = SellerResource,
                        increase = false
                    };

                    var ChangedresourceSeller = await new ResourcesProcesser().ChangeCompanyResourceAmount(changeSeller);

                    //-- Check if the seller could release their stock first
                    if (ChangedresourceSeller != false)
                    {
                        //-- Increase the company resource amount of the buyer
                        var changeBuyer = new ResourceAmountChangeModel
                        {
                            companyResource = BuyerResource,
                            increase = true
                        };

                        //- increase the for the resource count
                        var ChangedresourceBuyer = await new ResourcesProcesser().ChangeCompanyResourceAmount(changeBuyer);

                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
