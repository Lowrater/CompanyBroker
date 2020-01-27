using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using InteractWPF.DbConnect;
using InteractWPF.Interfaces;
using InteractWPF.Model;
using InteractWPF.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InteractWPF.ViewModel
{
   public class LogoutViewModel : ViewModelBase
    {
        private LogoutModel logoutModel = new LogoutModel();

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

            //-- Set's the date as first startup
            currentDateTime =  DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();

            //--- Calling async method in contructor
            //--- Sets the date every secod as long as we are connected.
            new Action(async () => await SetTime())();
        }

        //-- properties to the view 
        /// <summary>
        /// Time date
        /// </summary>
        public string currentDateTime
        {
            get => logoutModel._currentdatetime;
            set => Set(ref logoutModel._currentdatetime, value);
        }

        /// <summary>
        /// Sets the date every 10 seconds as long as the user is connected.
        /// </summary>
        /// <returns></returns>
        private async Task SetTime()
        {
            //-- While the user is active, update the timestamp
            while (dataService.msSQLUserInfo.IsConnected.Equals(true))
            {
                //-- waits 30 seconds 
                await Task.Delay(10000);
                //-- sets the currentDateTime property 
                currentDateTime = await Task.FromResult<string>(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString());
            }
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
