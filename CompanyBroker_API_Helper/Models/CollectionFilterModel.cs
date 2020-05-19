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
        public ObservableCollection<string> CompanyChoices { get; set; }
        public ObservableCollection<string> ProductTypeChoices { get; set; }
        public ObservableCollection<string> ProductNameChoices { get; set; }

        public string SearchWord { get; set; }
    }
}
