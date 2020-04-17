using CompanyBroker.Interfaces;
using CompanyBroker.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CompanyBroker.ViewModel
{
    public class SidePanelTab1ViewModel : ViewModelBase
    {
        //-------------------------------------------------------------------------- Models
        private SidePanelTab1Model sidePanelTab1Model = new SidePanelTab1Model();

        //-------------------------------------------------------------------------- Interfaces
        private IDBService _dBService;
        private IDataService _dataservice;
        private IAppConfigService _appConfigService;
        //-------------------------------------------------------------------------- Construcor
        public SidePanelTab1ViewModel(IDBService dBService, IDataService dataservice, IAppConfigService appConfigService)
        {
            this._dBService = dBService;
            this._dataservice = dataservice;
            this._appConfigService = appConfigService;

            //-- Fetches the companyList on startup
            FetchCompanyList();
            //-- Fetches the ResourceList on startup
            FetchResourceList();
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
        /// Item choosen from the CompanyList
        /// </summary>
        public string SelectedCompanyListItem
        {
            get => sidePanelTab1Model._selectedCompanyListItem;
            set
            {
                Set(ref sidePanelTab1Model._selectedCompanyListItem, value);
            }
        }

        //-------------------------------------------------------------------------- resources List
        /// <summary>
        /// List containing all resources types.
        /// </summary>
        public ObservableCollection<string> ResourceChoicesList
        {
            get => sidePanelTab1Model._resourceChoicesList;
            set
            {
                Set(ref sidePanelTab1Model._resourceChoicesList, value);
            }
        }

        /// <summary>
        /// List containing all resources types.
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
        /// Item choosen from the CompanyList
        /// </summary>
        public string SelectedProductListItem
        {
            get => sidePanelTab1Model._selectedProductListItem;
            set
            {
                Set(ref sidePanelTab1Model._selectedProductListItem, value);
            }
        }

        //--------------------------------------------------------------------- Check boxes

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



        //---------------------------------------------------------------- Methods
        /// <summary>
        /// Adds item to CompanyChoicesList type of Label
        /// </summary>
        public void AddToCompanyFilterList()
        {

        }

        /// <summary>
        /// Adds item to ResourceChoicesList type of Label
        /// </summary>
        public void AddToResourceFilterList()
        {

        }

        /// <summary>
        /// Sets the company list in the sidepanel
        /// </summary>
        public void FetchCompanyList()
        {
            CompanyList = _dBService.RequestCompanyList(_dataservice.msSQLUserInfo, _appConfigService.SQL_FetchCompanyList, _appConfigService.MSG_CannotConnectToServer);
        }

        public void FetchResourceList()
        {
            ProductTypeList = _dBService.RequestProductTypeList(_dataservice.msSQLUserInfo, _appConfigService.SQL_ProductTypeList, _appConfigService.MSG_CannotConnectToServer);
        }

    }
}
