using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker.Model
{
    public class AccountVModel
    {


        private string __companyName;
        public ref string _companyName => ref __companyName;


        private string __accountName;
        public ref string _accountName => ref __accountName;


        private int __totalListings;
        public ref int _totalListings => ref __totalListings;

        private int __soldListings;
        public ref int _soldListings => ref __soldListings;

        private int __inActiveListings;
        public ref int _inActiveListings => ref __inActiveListings;

        private int __activeUsers;
        public ref int _activeUsers => ref __activeUsers;

        private int __inActiveUsers;
        public ref int _inActiveUsers => ref __inActiveUsers;

        private decimal __companyBalance;
        public ref decimal _companyBalance => ref __companyBalance;
    }
}
