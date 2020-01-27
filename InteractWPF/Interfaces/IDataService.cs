using InteractWPF.DbConnect;
using InteractWPF.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractWPF.Interfaces
{
    /// <summary>
    /// Data service which contains all usable data which is needed to be shared or set by between viewmodels.
    /// </summary>
   public interface IDataService
    {
        MsSQLUserInfo msSQLUserInfo { get; set; }
    }
}
