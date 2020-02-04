using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CompanyBroker.DbConnect;
using CompanyBroker.Interfaces;
using CompanyBroker.Model;
using CompanyBroker.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CompanyBroker.ViewModel
{
   public class LogoutViewModel : ViewModelBase
    {
        //-- UI Commands
        public ICommand LogoutCommand => new RelayCommand(LogOut);

        //-- Interfaces
        private IDataService dataService;
        private IViewService viewService;

        /// <summary>
        /// COnstructor
        /// </summary>
        public LogoutViewModel(IDataService _dataService, IViewService _viewService)
        {
            this.dataService = _dataService;
            this.viewService = _viewService;
        }

        
        /// <summary>
        /// Logouts out, reset MsSQLUserInfo and returns to login window.
        /// </summary>
        public void LogOut()
        {
            //-- Resets the User informations
            dataService.msSQLUserInfo = new MsSQLUserInfo { DBuserName = "", IsConnected = false };

            //-- Creates new Login window
            viewService.CreateWindow(new LoginWindow());

            //-- Closes the Main window application window.
            foreach (Window window in Application.Current.Windows)
            {
                if(window.Title.Equals("MainWindow"))
                {
                    //-- Closes the window
                    window.Close();
                }                        
            }
        }
    }
}
