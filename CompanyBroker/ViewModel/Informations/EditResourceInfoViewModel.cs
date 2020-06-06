using CompanyBroker.Interfaces;
using CompanyBroker.Model.Informations;
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

namespace CompanyBroker.ViewModel.Informations
{
    public class EditResourceInfoViewModel : ViewModelBase
    {
        #region Model
        private ResourceInfoModel resourceInfoModel = new ResourceInfoModel();
        #endregion

        #region Interfaces
        private IDataService _dataservice;
        private IViewService _viewService;
        #endregion

        #region Icommands
        public ICommand CloseWindowCommand => new RelayCommand(CloseWindow);
        public ICommand SaveChangesCommand => new RelayCommand(async () => await SaveChanges());

        #endregion

        #region Construcor
        public EditResourceInfoViewModel(IDataService __dataservice, IViewService __viewService)
        {
            this._dataservice = __dataservice;
            this._viewService = __viewService;

            //-- fetches the informations on startup
            new Action(async () => await SetInformations())();
        }
        #endregion

        #region Properties

        public string ProductName
        {
            get => resourceInfoModel._productName;
            set => Set(ref resourceInfoModel._productName, value);
        }

        public string ProductType
        {
            get => resourceInfoModel._productType;
            set => Set(ref resourceInfoModel._productType, value);
        }
        public decimal ProductPrice
        {
            get => resourceInfoModel._productPrice;
            set => Set(ref resourceInfoModel._productPrice, value);
        }
        public string ProductDescription
        {
            get => resourceInfoModel._productDescription;
            set => Set(ref resourceInfoModel._productDescription, value);
        }

        public int ResourceId
        {
            get => resourceInfoModel._resourceId;
            set => Set(ref resourceInfoModel._resourceId, value);
        }

        public int ProductAmount
        {
            get => resourceInfoModel._productAmount;
            set => Set(ref resourceInfoModel._productAmount, value);
        }

        public bool ProductIsActive
        {
            get => resourceInfoModel._productIsActive;
            set => Set(ref resourceInfoModel._productIsActive, value);
        }

        private ResourcesModel Resource { get; set; }
        #endregion


        #region Methods
        /// <summary>
        /// Sets all the default informations on startup
        /// </summary>
        /// <returns></returns>
        private async Task SetInformations()
        {
            try
            {
                Resource = await new ResourcesProcesser().GetResourceByCompanyIdAndName(_dataservice.ResourceSelection.CompanyId, _dataservice.ResourceSelection.ProductName);
                var ResourceDescription = await new ResourcesProcesser().GetResourceDescription(_dataservice.ResourceSelection.ResourceId);

                if (Resource != null)
                {
                    ProductName = Resource.ProductName;
                    ProductType = Resource.ProductType;
                    ProductPrice = Resource.Price;
                    ResourceId = Resource.ResourceId;
                    ProductAmount = Resource.Amount;
                    ProductIsActive = Resource.Active;

                }

                if (ResourceDescription != null)
                {
                    ProductDescription = ResourceDescription.Description;
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
        /// Saves the changes based on the resource content
        /// </summary>
        /// <returns></returns>
        private async Task SaveChanges()
        {
            bool updateDescription = false;
            //-- Changes the values of the resource
            var updatedResource = Resource;
            updatedResource.Active = ProductIsActive;
            updatedResource.Amount = ProductAmount;
            updatedResource.Price = ProductPrice;
            updatedResource.ProductName = ProductName;
            updatedResource.ProductType = ProductType;

            try
            {
                //-- updates the default informations
                var resourceCheck = await new ResourcesProcesser().UpdateResourceInformations(updatedResource);

                //-- Checks if the resource description has been edited
                if(!string.IsNullOrEmpty(ProductDescription))
                {
                    //-- trys to update the description
                    updateDescription = await new ResourcesProcesser().UpdateProductDescription(ProductDescription, updatedResource.ResourceId, updatedResource.CompanyId);
                    //-- if the description fails, then it's because it doesn't exist. Trying to create it.
                    if (updateDescription == false)
                    {
                        updateDescription = await new ResourcesProcesser().AddProductDescription(ProductDescription, updatedResource.ResourceId, updatedResource.ResourceId);
                    }
                }
                

                if (resourceCheck != false)
                {
                    if(resourceCheck == updateDescription)
                    {
                        //-- Messages 
                        MessageBox.Show($"Product updated successful!",
                                        "CompanyBroker: resource error creation",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                    }
                    else
                    {
                        //-- Messages 
                        MessageBox.Show($"Product updated without description successful!",
                                        "CompanyBroker: resource error creation",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                    }
                }
                else
                {
                    //-- Messages 
                    MessageBox.Show($"Could not update product informations for {ProductName}",
                                    "CompanyBroker: resource error creation",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
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
        /// Closes the window
        /// </summary>
        public void CloseWindow()
        {
            _viewService.CloseWindow("EditResourceInfo");
        }
        #endregion
    }
}
