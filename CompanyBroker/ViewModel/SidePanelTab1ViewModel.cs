using CompanyBroker.Interfaces;
using CompanyBroker.Model;
using CompanyBroker_API_Helper.Models;
using CompanyBroker_API_Helper.Processers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyBroker.ViewModel
{

    public class SidePanelTab1ViewModel : ViewModelBase
    {
        #region Model
        private SidePanelTab1Model sidePanelTab1Model = new SidePanelTab1Model();
        #endregion

        #region Interfaces
        private IDataService _dataservice;
        private IAppConfigService _appConfigService;
        private IContentService _contentService;
        #endregion

        #region Icommands
        public ICommand ResetComand => new RelayCommand(ResetSidePanel);

        #endregion

        #region Construcor
        public SidePanelTab1ViewModel(IDataService __dataservice, IAppConfigService __appConfigService, IContentService __contentService)
        {
            this._dataservice = __dataservice;
            this._appConfigService = __appConfigService;
            this._contentService = __contentService;

            //-- Fetches the companyList on startup by async incase the task takes time, to not lock the user
            //new Action(async () => CompanyList = await FetchCompanyList())();
            new Action(async () => Companies = await FetchCompaniesList())();
            //-- Fetches the ResourceList on startup by async incase the task takes time, to not lock the user
            new Action(async () => ProductTypeList = await FetchProductTypeList())();
        }
        #endregion



        #region Properties

        //// Company List
        ///// <summary>
        ///// List containing all company names
        ///// </summary>
        public ObservableCollection<CompanyModel> Companies
        {
            get => sidePanelTab1Model._companyList;
            set
            {
                Set(ref sidePanelTab1Model._companyList, value);
            }
        }

        /// <summary>
        /// List containing all Company choices (For user perspective)
        /// </summary>
        public ObservableCollection<CompanyModel> CompanyChoicesList
        {
            get => sidePanelTab1Model._companyChoicesList;
            set
            {
                Set(ref sidePanelTab1Model._companyChoicesList, value);
            }
        }

        /// <summary>
        // List containing all Product Types choices added to the list for filtering.
        /// </summary>
        public ObservableCollection<string> ProductTypeList
        {
            get => sidePanelTab1Model._productTypeList;
            set
            {
                Set(ref sidePanelTab1Model._productTypeList, value);
            }
        }

        /// <summary>
        /// List containing all Product types. etc. electronic 
        /// </summary>
        public ObservableCollection<string> ProductTypeChoicesList
        {
            get => sidePanelTab1Model._productTypeChoicesList;
            set
            {
                Set(ref sidePanelTab1Model._productTypeChoicesList, value);
            }
        }



        /// <summary>
        /// List containing all product names existing depending om the product type selection
        /// Uses a query to fill this list
        /// </summary>
        public ObservableCollection<string> ProductNameList
        {
            get => sidePanelTab1Model._productNameList;
            set
            {
                Set(ref sidePanelTab1Model._productNameList, value);
            }
        }


        /// <summary>
        /// List containing all product names choosen, from ProductTypeList
        /// </summary>
        public ObservableCollection<string> ProductNameChoicesList
        {
            get => sidePanelTab1Model._productNameChoicesList;
            set
            {
                Set(ref sidePanelTab1Model._productNameChoicesList, value);
            }
        }

        //-------------------------------------------------------------------- Selected items
        /// <summary>
        /// Selected value for Companies and CompanyChoicesList
        /// </summary>
        public CompanyModel SelectedCompany
        {
            get => sidePanelTab1Model._selectedCompany;
            set
            {
                Set(ref sidePanelTab1Model._selectedCompany, value);
                //-- Updates the CompanyChoicesList
                CompanyChoicesList.Add(value);
                //-- Sets the collection list
                SetListCollection();
            }
        }

        public CompanyModel SelectedCompanyChoices
        {
            get => sidePanelTab1Model._selectedCompanyChoices;
            set
            {
                Set(ref sidePanelTab1Model._selectedCompanyChoices, value);
                //-- Updates the CompanyChoicesList
                CompanyChoicesList.Remove(value);
                //-- Sets the collection list
                SetListCollection();
            }
        }

        /// <summary>
        /// Item choosen from the ProductList
        /// </summary>
        public string SelectedProductListItem
        {
            get => sidePanelTab1Model._selectedProductListItem;
            set
            {
                Set(ref sidePanelTab1Model._selectedProductListItem, value);
                //-- Adds the selected item from the ProductNameList to the ProductTypeChoicesList for filtering in SQL
                ProductTypeChoicesList.Add(value);
                //-- Updates the ProductNameList based on the content in ProductTypeChoicesList list
                new Action(async () => ProductNameList = await FetchProductNameList())();
                //-- Sets the collection list
                SetListCollection();
            }
        }


        /// <summary>
        /// Item choosen from the ProductNameList
        /// </summary>
        public string SelectedProductNameListItem
        {
            get => sidePanelTab1Model._selectedProductNameListItem;
            set
            {
                Set(ref sidePanelTab1Model._selectedProductNameListItem, value);
                //-- Adds the selected item from the ProductNameList to the ProductNameChoicesList for filtering in SQL
                _contentService.AddSelectedListItem(ProductNameChoicesList, value);
                //-- Sets the collection list
                SetListCollection();
            }
        }

        //--------------------------------------------------------------------- Check boxes - START

        /// <summary>
        /// Bool
        /// If the search only needs to be a firm whom we are partners with
        /// </summary>
        public bool PartnersOnly
        {
            get => sidePanelTab1Model._partnersOnly;
            set
            {
                Set(ref sidePanelTab1Model._partnersOnly, value);
                //-- Sets the collection list
                SetListCollection();
            }
        }

        /// <summary>
        /// bool
        /// If we want to search for resources that can be bought in bulks
        /// </summary>
        public bool BulkBuy
        {
            get => sidePanelTab1Model._bulkBuy;
            set
            {
                Set(ref sidePanelTab1Model._bulkBuy, value);
                //-- Sets the collection list
                SetListCollection();
            }
        }

        /// <summary>
        /// Lowest price filter
        /// </summary>
        public decimal LowestPrice
        {
            get => sidePanelTab1Model._lowestPrice;
            set
            {
                Set(ref sidePanelTab1Model._lowestPrice, value);
                //-- Sets the collection list
                SetListCollection();
            }
        }

        /// <summary>
        /// Highest price filter
        /// </summary>
        public decimal HigestPrice
        {
            get => sidePanelTab1Model._higestPrice;
            set
            {
                Set(ref sidePanelTab1Model._higestPrice, value);
                //-- Sets the collection list
                SetListCollection();
            }
        }

        //--------------------------------------------------------------------- Check boxes - END

        /// <summary>
        /// Used to remove an selected index on SelectedIndex on ProductTypeChoicesList
        /// </summary>
        public string RemoveProductTypeChoicesIndex
        {
            get => sidePanelTab1Model._removeListItem;
            set
            {
                Set(ref sidePanelTab1Model._removeListItem, value);
                ////-- Removes the element at the index selected
                ProductTypeChoicesList.Remove(value);
                //-- Sets the collection list
                SetListCollection();

            }
        }

        /// <summary>
        /// Used to remove an selected index on SelectedIndex on ProductTypeChoicesList
        /// </summary>
        public string RemoveProductNameChoiceIndex
        {
            get => sidePanelTab1Model._removeListItem;
            set
            {
                Set(ref sidePanelTab1Model._removeListItem, value);
                ////-- Removes the element at the index selected
                ProductNameChoicesList.Remove(value);
                //-- Updates the ProductNameList based on the content in ProductTypeChoicesList list
                new Action(async () => ProductNameList = await FetchProductNameList())();
                //-- Sets the collection list
                SetListCollection();
            }
        }

        #endregion

        #region Methods

        //---------------------------------------------------------------- Methods

        /// <summary>
        /// Sets the company list in the sidepanel
        /// </summary>
        public async Task<ObservableCollection<CompanyModel>> FetchCompaniesList()
        {
            //-- Fetches all the company data
            return await new CompanyProcesser().GetAllCompanies();
        }

        /// <summary>
        /// Sets the resource list
        /// </summary>
        public async Task<ObservableCollection<string>> FetchProductTypeList()
        {
            return await new ResourcesProcesser().GetAllProductTypes();
        }

        /// <summary>
        /// Sets the productname list based on the choosen product type. Uses ProductTypeChoicesList
        /// </summary>
        public async Task<ObservableCollection<string>> FetchProductNameList()
        {
            return await new ResourcesProcesser().GetAllProductNamesByType(ProductTypeChoicesList);
        }


        /// <summary>
        /// Sets the list collection in the DataService, to provide data for the Search Engine in BrokerOverViewModel for API calls
        /// Is being called every time one of the choices lists has been updated.
        /// </summary>
        public void SetListCollection()
        {

            _dataservice.FilterCollection = new CollectionFilterModel
            {
                CompanyChoices = CompanyChoicesList.Select(c => c.Id).ToArray(),
                ProductTypeChoices = ProductTypeChoicesList.ToArray(),
                ProductNameChoices = ProductNameChoicesList.ToArray(),
                LowestPriceChoice = LowestPrice,
                HigestPriceChoice = HigestPrice,
                BulkChoice = BulkBuy,
                Partners_OnlyChoice = PartnersOnly
            };

        }

        /// <summary>
        /// Resets everything in the sidePanel
        /// </summary>
        public void ResetSidePanel()
        {
            PartnersOnly = false;
            BulkBuy = false;
            ProductNameList = new ObservableCollection<string>();
            ProductNameChoicesList = new ObservableCollection<string>();
            CompanyChoicesList = new ObservableCollection<CompanyModel>();
            ProductTypeChoicesList = new ObservableCollection<string>();
            HigestPrice = 0;
            LowestPrice = 0;
            //-- Date here
        }
        #endregion
    }
}
