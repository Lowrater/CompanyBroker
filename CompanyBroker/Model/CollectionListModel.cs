using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker.Model
{
    public class CollectionListModel
    {
        public ObservableCollection<string> CompanyChoices { get; set; }
        public ObservableCollection<string> ProductTypeChoices { get; set; }

        public ObservableCollection<string> ProductNameChoicesList { get; set; }
        

    }
}
