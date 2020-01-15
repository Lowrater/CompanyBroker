using InteractWPF.Interfaces;
using InteractWPF.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InteractWPF.Services
{
   public class ViewService : IViewService
    {

        /// <summary>
        /// Takes any Window parameter from the View/Windows to open it.
        /// Every ViewModel attached will be applyed when shown.
        /// </summary>
        /// <param name="window"></param>
        public void CreateWindow(Window window)
        {
            window.Show();
        }
    }
}
