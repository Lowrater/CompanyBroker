using CompanyBroker.Interfaces;
using CompanyBroker.Model.AccountModels;
using CompanyBroker_API_Helper.Models;
using CompanyBroker_API_Helper.Processers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CompanyBroker.ViewModel.AccountControl
{
    public class CompanyAddListingViewModel : ViewModelBase
    {
        #region Models
        private CompanyAddListingModel companyAddListingModel = new CompanyAddListingModel();
        #endregion

        #region Interfaces
        IDataService _dataservice;
        IViewService _viewservice;
        #endregion

        #region ICommands
        public ICommand CloseWindowCommand => new RelayCommand(CloseWindow);
        public ICommand AddListingCommand => new RelayCommand(async () => await AddListing());
        #endregion

        #region Constructor
        public CompanyAddListingViewModel(IDataService __dataservice, IViewService __viewservice)
        {
            this._dataservice = __dataservice;
            this._viewservice = __viewservice;
        }
        #endregion

        #region Properties
        public string ProductName
        { 
            get => companyAddListingModel._productName;
            set => Set(ref companyAddListingModel._productName, value); 
        }

        public string ProductType
        {
            get => companyAddListingModel._productType;
            set => Set(ref companyAddListingModel._productType, value);
        }
        public int ProductAmount
        {
            get => companyAddListingModel._productAmount;
            set => Set(ref companyAddListingModel._productAmount, value);
        }
        public decimal ProductPrice
        {
            get => companyAddListingModel._productPrice;
            set => Set(ref companyAddListingModel._productPrice, value);
        }
        public bool ProductActive
        {
            get => companyAddListingModel._productActive;
            set => Set(ref companyAddListingModel._productActive, value);
        }
        public string ProductDescription
        {
            get => companyAddListingModel._productDescription;
            set => Set(ref companyAddListingModel._productDescription, value);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Closes the the window
        /// </summary>
        public void CloseWindow()
        {
            _viewservice.CloseWindow("AddListingWndow");
        }

        /// <summary>
        /// Adds the informations to the database
        /// </summary>
        /// <returns></returns>
        public async Task AddListing()
        {
            //-- creates a new resource
            var resource = new ResourcesModel
            {
                Active = ProductActive,
                Amount = ProductAmount,
                CompanyId = _dataservice.account.CompanyId,
                Price = ProductPrice,
                ProductName = ProductName,
                ProductType = ProductType
            };

            try
            {
                //-- Adds the new resource
                var addListing = await new ResourcesProcesser().AddNewResources(resource);

                //- verifys the result
                if (addListing != false)
                {

                    //-- Fetches the new resource
                    var newResource = await new ResourcesProcesser().GetResourceByCompanyIdAndName(_dataservice.account.CompanyId, ProductName);

                    if(newResource != null)
                    {
                        //-- Creates an new resource description
                        var des = new ResourceDescriptionModel
                        {
                            Description = ProductDescription,
                            ResourceId = newResource.ResourceId
                        };

                        //-- Adds the descripion to the product
                        addListing = await new ResourcesProcesser().AddProductDescription(des);

                        if (addListing != false)
                        {
                            //-- Messages
                            MessageBox.Show($"Resource {ProductName} successfully created",
                                            "CompanyBroker: resource  creation",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Information);
                        }
                        else
                        {
                            //-- Messages
                            MessageBox.Show($"Resource {ProductName} successfully created, but description failed",
                                            "CompanyBroker: resource  creation",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        //-- Messages
                        MessageBox.Show($"Resource {ProductName} successfully created, but description failed",
                                        "CompanyBroker: resource  creation",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                    }
                }
                else
                {
                    //-- Messages
                    MessageBox.Show($"Resource {ProductName} failed to be created",
                                    "CompanyBroker: resource error creation",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }
            }
            catch (Exception e)
            {
                //-- Messages 
                MessageBox.Show(e.ToString().Substring(0,252),
                                "CompanyBroker: resource error creation",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }
        #endregion
    }
}
