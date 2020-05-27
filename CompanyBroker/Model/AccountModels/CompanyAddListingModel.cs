using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker.Model.AccountModels
{
    public class CompanyAddListingModel
    {
        private string __productName;
        public ref string _productName => ref __productName;

        private string __productType;
        public ref string _productType => ref __productType;

        private int __productAmount;
        public ref int _productAmount => ref __productAmount;


        private decimal __productPrice;
        public ref decimal _productPrice => ref __productPrice;


        private bool __productActive;
        public ref bool _productActive => ref __productActive;

        private string __productDescription;
        public ref string _productDescription => ref __productDescription;
        

    }
}
