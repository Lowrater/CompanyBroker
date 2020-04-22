using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker.Model
{
    public class CreateAccountModel
    {
        private bool __newCompanyBool;
        public ref bool _newCompanyBool => ref __newCompanyBool;

        private bool __companyDropDownBool;
        public ref bool _companyDropDownBool => ref __companyDropDownBool;

        private bool __companyNameBool;
        public ref bool _companyNameBool => ref __companyNameBool;

        

        private string __companyName;
        public ref string _companyName => ref __companyName;

        private string __accountName;
        public ref string _accountName => ref __accountName;

        private int __companyChoice;
        public ref int _companyChoice => ref __companyChoice;

        private string __accountEmail;
        public ref string _accountEmail => ref __accountEmail;

        private ObservableCollection<string> __companyList = new ObservableCollection<string>();
        public ref ObservableCollection<string> _companyList => ref __companyList;
    }
}
