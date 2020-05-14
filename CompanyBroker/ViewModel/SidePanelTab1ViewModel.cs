using CompanyBroker.Interfaces;
using CompanyBroker.Model;
using CompanyBroker_API_Helper.Models;
using CompanyBroker_API_Helper.Processers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyBroker.ViewModel
{
    public class SidePanelTab1ViewModel : ViewModelBase
    {
        //-------------------------------------------------------------------------- Models
        private SidePanelTab1Model sidePanelTab1Model = new SidePanelTab1Model();

        //-------------------------------------------------------------------------- Interfaces
        private IDataService _dataservice;
        private IAppConfigService _appConfigService;
        private IContentService _contentService;

        //-------------------------------------------------------------------------- Icommands
        public ICommand ResetComand => new RelayCommand(ResetSidePanel);

        //-------------------------------------------------------------------------- Construcor
        public SidePanelTab1ViewModel(IDataService __dataservice, IAppConfigService __appConfigService, IContentService __contentService)
        {
            this._dataservice = __dataservice;
            this._appConfigService = __appConfigService;
            this._contentService = __contentService;

            //-- Fetches the companyList on startup by async incase the task takes time, to not lock the user
            new Action(async () => CompanyList = await FetchCompanyList())();
            //-- Fetches the ResourceList on startup by async incase the task takes time, to not lock the user
            new Action(async () => ProductTypeList = await FetchProductTypeList())();

        }


        //-------------------------------------------------------------------------- Properties
        // Company List
        /// <summary>
        /// List containing all company names
        /// </summary>
        public ObservableCollection<string> CompanyList
            {
                get => sidePanelTab1Model._companyList;
                set
                {
                    Set(ref sidePanelTab1Model._companyList, value);
                }
           }


        /// <summary>
        /// List containing all Company choices added to the list for filtering.
        /// </summary>
        public ObservableCollection<string> CompanyChoicesList
        {
            get => sidePanelTab1Model._companyChoicesList;
            set
            {
                Set(ref sidePanelTab1Model._companyChoicesList, value);
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
        /// List containing all product names choosen, from ProductNameList
        /// </summary>
        public ObservableCollection<string> ProductNameChoicesList
        {
            get => sidePanelTab1Model._productNameChoicesList;
            set
            {
                Set(ref sidePanelTab1Model._productNameChoicesList, value);
            }
        }

        

        /// <summary>
        /// Item choosen from the CompanyList
        /// </summary>
        public string SelectedCompanyListItem
        {
            get => sidePanelTab1Model._selectedCompanyListItem;
            set
            {
                Set(ref sidePanelTab1Model._selectedCompanyListItem, value);
                //-- Adds the selected item from the CompanyList to the CompanyChoicesList for filtering in SQL
                _contentService.AddSelectedListItem(CompanyChoicesList, value);
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
                _contentService.AddSelectedListItem(ProductTypeChoicesList, value);
                //-- Updates the ProductNameList based on the content in ProductTypeChoicesList list
                new Action(async () => ProductNameList = await FetchProductNameList())();
               
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
                _contentService.RemoveSelectedListIndex(ProductTypeChoicesList, value);
            }
        }

        /// <summary>
        /// Used to remove an selected index on SelectedIndex on ProductTypeChoicesList
        /// </summary>
        public string RemoveCompanyChoicesIndex
        {
            get => sidePanelTab1Model._removeListItem;
            set
            {
                Set(ref sidePanelTab1Model._removeListItem, value);
                ////-- Removes the element at the index selected
                _contentService.RemoveSelectedListIndex(CompanyChoicesList, value);
            }
        }

        /// <summary>
        /// Used to remove an selected index on SelectedIndex on ProductTypeChoicesList
        /// </summary>
        public string SelectedProductNameChoiceIndex
        {
            get => sidePanelTab1Model._removeListItem;
            set
            {
                Set(ref sidePanelTab1Model._removeListItem, value);
                ////-- Removes the element at the index selected
                _contentService.RemoveSelectedListIndex(ProductNameChoicesList, value);
            }
        }


        //---------------------------------------------------------------- Methods

        /// <summary>
        /// Sets the company list in the sidepanel
        /// </summary>
        public async Task<ObservableCollection<string>> FetchCompanyList()
        {
            //-- Creates a new list with strings
            var companyList = new ObservableCollection<string>();
            //-- Fetches all the company data
            var list = await new CompanyProcesser().GetAllCompanies();
            //-- loops through the list and adds only the name of the data.
            foreach (CompanyModel company in list)
            {
                companyList.Add(company.Id + " " + company.Name);
            }
            //-- returns the list
            return companyList;
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
        /// Resets everything in the sidePanel
        /// </summary>
        public void ResetSidePanel()
        {
            PartnersOnly = false;
            BulkBuy = false;
            ProductNameList = new ObservableCollection<string>();
            ProductTypeList = new ObservableCollection<string>();
            CompanyList = new ObservableCollection<string>();
            ProductNameChoicesList = new ObservableCollection<string>();
            CompanyChoicesList = new ObservableCollection<string>();
            ProductTypeChoicesList = new ObservableCollection<string>();

            //-- Price here
            //-- Date here
            //-- Expire date here


        }

    }
}
