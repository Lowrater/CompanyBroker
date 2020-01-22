using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractWPF.Model
{
    public class LogoutModel
    {
        public ref string _currentdatetime => ref __currentdatetime;
        private string __currentdatetime;
    }
}
