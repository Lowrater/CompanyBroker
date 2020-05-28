using CompanyBroker.Interfaces;
using CompanyBroker.Model.Informations;
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
    public class ResourceInfoViewModel : ViewModelBase
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

        #endregion

        #region Construcor
        public ResourceInfoViewModel(IDataService __dataservice, IViewService __viewService)
        {
            this._dataservice = __dataservice;
            this._viewService = __viewService;

            //-- fetches the informations on startup
            new Action(async () => await SetInformations())();
        }
        #endregion

        #region Properties

        public string CompanyName
        {
            get => resourceInfoModel._companyName;
            set => Set(ref resourceInfoModel._companyName, value);
        }

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
                var Company = await new CompanyProcesser().GetCompanyById(_dataservice.ResourceSelection.CompanyId);
                var Resource = await new ResourcesProcesser().GetResourceByCompanyIdAndName(_dataservice.ResourceSelection.CompanyId, _dataservice.ResourceSelection.ProductName);
                var ResourceDescription = await new ResourcesProcesser().GetResourceDescription(_dataservice.ResourceSelection.ResourceId);

                CompanyName = Company.Name;
                ProductName = Resource.ProductName;
                ProductType = Resource.ProductType;
                ProductPrice = Resource.Price;
                ProductDescription = ResourceDescription.Description;

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
            _viewService.CloseWindow("ResourceInfo");
        }
        #endregion
    }
}
