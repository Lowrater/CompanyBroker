using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CompanyBroker.Interfaces;
using CompanyBroker.View.Windows;
using System.Windows;
using System.Windows.Input;
using System.Linq;

namespace CompanyBroker.ViewModel
{
    public class LogoutViewModel : ViewModelBase
    {
        //------------------------------------------------------------------------------------------------UI Commands
        public ICommand LogoutCommand => new RelayCommand(LogOut);
        public ICommand AccountCommand => new RelayCommand(OpenAccountWindow);

        //------------------------------------------------------------------------------------------------ Interfaces
        private IDataService _dataService;
        private IViewService _viewService;


        //------------------------------------------------------------------------------------------------ Constructor
        /// <summary>
        /// COnstructor
        /// </summary>
        public LogoutViewModel(IDataService __dataService, IViewService __viewService)
        {
            this._dataService = __dataService;
            this._viewService = __viewService;
        }

        //------------------------------------------------------------------------------------------------ Methods

        /// <summary>
        /// Creates an new instance of the AccountWindow
        /// </summary>
        public void OpenAccountWindow()
        {
           _viewService.CreateWindow(new AccountWindow());
        }

        /// <summary>
        /// Logouts out, reset MsSQLUserInfo and returns to login window.
        /// </summary>
        public void LogOut()
        {
            //-- Resets the User informations
            _dataService.isConnected = false;

            //-- Creates new Login window
            _viewService.CreateWindow(new LoginWindow());

            //-- Closes the Main window application window.
            _viewService.CloseWindow("TheMainWindow");

        }
    }
}
