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
        private ObservableCollection<string> __companyList = new ObservableCollection<string>();
        public ref ObservableCollection<string>  _companyList => ref __companyList;

        //-- resource list
        private ObservableCollection<string> __productTypeList = new ObservableCollection<string>();
        public ref ObservableCollection<string> _productTypeList => ref __productTypeList;

        //- Choices of companys to filter with
        private ObservableCollection<string> __companyChoicesList = new ObservableCollection<string>();
        public ref ObservableCollection<string> _companyChoicesList => ref __companyChoicesList;

        //-- choices of resource to choose from
        private ObservableCollection<string> __productTypeChoicesList = new ObservableCollection<string>();
        public ref ObservableCollection<string> _productTypeChoicesList => ref __productTypeChoicesList;


        //-- choices of resource to choose from
        private ObservableCollection<string> __productNameChoicesList = new ObservableCollection<string>();
        public ref ObservableCollection<string> _productNameChoicesList => ref __productNameChoicesList;


        private ObservableCollection<string> __productNameList = new ObservableCollection<string>();
        public ref ObservableCollection<string> _productNameList => ref __productNameList;

        



        //-- Item choosen from the company list
        private string __selectedCompanyListItem;
        public ref string _selectedCompanyListItem => ref __selectedCompanyListItem;

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
