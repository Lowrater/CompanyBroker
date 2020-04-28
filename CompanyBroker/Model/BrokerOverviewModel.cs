using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker.Model
{
    public class BrokerOverviewModel
    {
        private DataTable __mainTable;
        public ref DataTable _mainTable => ref __mainTable;
    }
}
