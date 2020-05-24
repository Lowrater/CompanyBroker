﻿using CompanyBroker.Interfaces;
using CompanyBroker.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CompanyBroker.Services
{
   public class ViewService : IViewService
    {

        /// <summary>
        /// Takes any Window parameter from the View/Windows to open it.
        /// Every ViewModel attached will be applyed when shown.
        /// </summary>
        /// <param name="window"></param>
        public void CreateWindow(Window theWindow)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (!window.Name.Equals(theWindow.Name))
                {
                    //-- Displays the window
                    theWindow.Show();
                }
            }
        }

        /// <summary>
        /// Closes all windows with the following window title name.
        /// </summary>
        /// <param name="windowTitle"></param>
        public void CloseWindow(string windowName)
        {
            //-- Closes the CreateAccountWindow window
            foreach (Window window in Application.Current.Windows)
            {
                //-- Searches for a window with the following CreateAccountWindow to remove it
                if (window.Name.Equals(windowName))
                {
                    window.Close();
                }
            }
        }
    }
}
