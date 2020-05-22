using CompanyBroker_API_Helper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CompanyBroker.Model
{
    public class SidePanelTab1Model
    {
        //-- Company list
        private ObservableCollection<CompanyModel> __companyList = new ObservableCollection<CompanyModel>();
        public ref ObservableCollection<CompanyModel> _companyList => ref __companyList;

        //-- resource list
        private ObservableCollection<string> __productTypeList = new ObservableCollection<string>();
        public ref ObservableCollection<string> _productTypeList => ref __productTypeList;

        //- Choices of companys to filter with
        private ObservableCollection<CompanyModel> __companyChoicesList = new ObservableCollection<CompanyModel>();
        public ref ObservableCollection<CompanyModel> _companyChoicesList => ref __companyChoicesList;

        //-- choices of resource to choose from
        private ObservableCollection<string> __productTypeChoicesList = new ObservableCollection<string>();
        public ref ObservableCollection<string> _productTypeChoicesList => ref __productTypeChoicesList;


        //-- choices of resource to choose from
        private ObservableCollection<string> __productNameChoicesList = new ObservableCollection<string>();
        public ref ObservableCollection<string> _productNameChoicesList => ref __productNameChoicesList;


        private ObservableCollection<string> __productNameList = new ObservableCollection<string>();
        public ref ObservableCollection<string> _productNameList => ref __productNameList;


        //-- companies
        private CompanyModel __selectedCompany;
        public ref CompanyModel _selectedCompany => ref __selectedCompany;

        private CompanyModel __selectedCompanyChoices;
        public ref CompanyModel _selectedCompanyChoices => ref __selectedCompanyChoices;
        

        //-- Prices

        private decimal __lowestPrice;
        public ref decimal _lowestPrice => ref __lowestPrice;

        private decimal __higestPrice;
        public ref decimal _higestPrice => ref __higestPrice;

        //-- Item choosen from the company list
        private string __selectedProductListItem;
        public ref string _selectedProductListItem => ref __selectedProductListItem;

        private string __selectedProductNameListItem;
        public ref string _selectedProductNameListItem => ref __selectedProductNameListItem;
        

        private string __removeListItem;
        public ref string _removeListItem => ref __removeListItem;

        //-- partners only check box
        private bool __partnersOnly;
        public ref bool _partnersOnly => ref __partnersOnly; 

        //-- bulk check box
        private  bool __bulkBuy;
        public ref bool _bulkBuy => ref __bulkBuy;

    }
}
