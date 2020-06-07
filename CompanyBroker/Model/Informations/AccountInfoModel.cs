using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker.Model.Informations
{
    public class AccountInfoModel
    {
        private string __accountName;
        public ref string _accountName => ref __accountName;

        private string __accountEmail;
        public ref string _accountEmail => ref __accountEmail;

        private bool __isActive;
        public ref bool _isActive => ref __isActive;
    }
}
