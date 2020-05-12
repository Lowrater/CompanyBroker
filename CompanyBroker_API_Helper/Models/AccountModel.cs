using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker_API_Helper.Models
{
    public class AccountModel
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }
}
