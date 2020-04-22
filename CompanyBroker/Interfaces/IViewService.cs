using CompanyBroker.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CompanyBroker.Interfaces
{
    /// <summary>
    /// Contains all View manipulations and usage
    /// </summary>
   public interface IViewService
    {

        //-- Windows
        void CreateWindow(Window window);

        void CloseWindow(string windowTitle);

    }
}
