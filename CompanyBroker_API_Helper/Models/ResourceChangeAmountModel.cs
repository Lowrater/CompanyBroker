using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker_API_Helper.Models
{
   public class ResourceChangeAmountModel
    {
        public bool IncreaseAmount { get; set; }
        public int companyId { get; set; }
        public int resourceId { get; set; }
    }
}
