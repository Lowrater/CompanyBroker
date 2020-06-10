using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker_API_Helper.Models
{
    public class CollectionFilterModel
    {
        public int[] CompanyChoices { get; set; }
        public string[] ProductTypeChoices { get; set; }
        public string[] ProductNameChoices { get; set; }
        public string SearchWord { get; set; }
        public bool Partners_OnlyChoice { get; set; }
        public bool ResourceActive { get; set; }
        public decimal LowestPriceChoice {get; set;}
        public decimal HigestPriceChoice { get; set; }

    }
}
