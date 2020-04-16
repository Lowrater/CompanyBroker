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
        private ObservableCollection<string> __companyList;
        public ref ObservableCollection<string>  _companyList => ref __companyList;

        //-- resource list
        private ObservableCollection<string> __resourceList;
        public ref ObservableCollection<string> _resourceList => ref __resourceList;

        //- Choices of companys to filter with
        private ObservableCollection<string> __companyChoicesList;
        public ref ObservableCollection<string> _companyChoicesList => ref __companyChoicesList;

        //-- choices of resource to choose from
        private ObservableCollection<string> __resourceChoicesList;
        public ref ObservableCollection<string> _resourceChoicesList => ref __resourceChoicesList;

        //-- Item choosen from the company list
        private string __selectedCompanyListItem;
        public ref string _selectedCompanyListItem => ref __selectedCompanyListItem;

        //-- Item choosen from the company list
        private string __selectedResourceListItem;
        public ref string _selectedResourceListItem => ref __selectedResourceListItem;

      

        //-- partners only check box
        private bool __partnersOnly;
        public ref bool _partnersOnly => ref __partnersOnly; 

        //-- bulk check box
        private  bool __bulkBuy;
        public ref bool _bulkBuy => ref __bulkBuy;

    }
}
