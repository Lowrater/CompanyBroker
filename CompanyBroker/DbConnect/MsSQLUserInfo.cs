using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker.DbConnect
{
    /// <summary>
    /// Stores all relevant information about the user.
    /// In progress.
    /// </summary>
   public class MsSQLUserInfo
    {
        public string DBuserName { get; set; }
        public bool IsConnected { get; set; }
        public string connectionstring { get; set; }
    }
}
