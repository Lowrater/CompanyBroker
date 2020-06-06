using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker_API_Helper.Models
{
    public class ResourceDescriptionModel
    {
        public int ResourceId { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }

    }
}
