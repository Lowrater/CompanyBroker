using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CompanyBroker_API_Helper.Models
{
    public class ResourcesModel
    {
        public int ResourceId { get; set; }
        public int CompanyId { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
    }
}
