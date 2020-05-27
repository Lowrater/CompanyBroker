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
        #region Models
        #endregion

        #region Interfaces
        private IDataService _dataService;
        private IViewService _viewService;
        #endregion

        #region ICommands
        public ICommand AccountCommand => new RelayCommand(OpenAccountWindow);
        public ICommand LogoutCommand => new RelayCommand(LogOut);

        #endregion

        #region Constructor
        /// <summary>
        /// COnstructor
        /// </summary>
        public LogoutViewModel(IDataService __dataService, IViewService __viewService)
        {
            this._dataService = __dataService;
            this._viewService = __viewService;
        }
        #endregion

        #region Properties
        #endregion

        #region Methods

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
        #endregion

    }
}
