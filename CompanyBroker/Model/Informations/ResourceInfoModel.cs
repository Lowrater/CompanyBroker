using CompanyBroker_API_Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker.Model.Informations
{
    public class ResourceInfoModel
    {
        private string __companyName;
        public ref string _companyName => ref __companyName;
        
        private string __productName;
        public ref string _productName => ref __productName;

        private string __productType;
        public ref string _productType => ref __productType;

        private decimal __productPrice;
        public ref decimal _productPrice => ref __productPrice;

        private string __productDescription;
        public ref string _productDescription => ref __productDescription;

        private int __resourceId;
        public ref int _resourceId => ref __resourceId;

        private bool __productIsActive;
        public ref bool _productIsActive => ref __productIsActive;

        private int __productAmount;
        public ref int _productAmount => ref __productAmount;
        

    }
}
