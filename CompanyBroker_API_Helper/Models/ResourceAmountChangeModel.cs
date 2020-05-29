using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker_API_Helper.Models
{
   public class ResourceAmountChangeModel
    {

        public ResourcesModel companyResource { get; set; }
        public bool increase { get; set; }
    }
}
