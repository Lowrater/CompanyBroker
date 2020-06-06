using CompanyBroker.Interfaces;
using CompanyBroker.Model;
using CompanyBroker_API_Helper;
using CompanyBroker_API_Helper.Models;
using CompanyBroker_API_Helper.Processers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyBroker.ViewModel
{
    public class AccountViewModel : ViewModelBase
    {
        #region Models
        private AccountVModel accountVModel = new AccountVModel();
        #endregion

        #region Interfaces
        private IViewService _viewService;
        private IDataService _dataService;
        #endregion

        #region ICommands
        public ICommand CloseWindowCommand => new RelayCommand(CloseWindow);
        public ICommand ChangePasswordCommand => new RelayCommand(ChangePassword);
        public ICommand ChangePictureCommand => new RelayCommand(ChangePicture);
        public ICommand EditCompanyCommand => new RelayCommand(EditCompany);
        public ICommand CloseFirmCommand => new RelayCommand(CloseFirm);
        #endregion

        #region Constructor
        public AccountViewModel(IViewService __viewService, IDataService __dataService)
        {
            //-- Dependency injections (Constructor injection)
            this._viewService = __viewService;
            this._dataService = __dataService;

            //-- Sets the account informations
            new Action(async () => await SetAccountInformations())();
        }
        #endregion


        #region Properties

        public string CompanyName 
        { 
            get => accountVModel._companyName; 
            set => Set(ref accountVModel._companyName, value);
        }

        public string AccountName
        {
            get => accountVModel._companyName;
            set => Set(ref accountVModel._companyName, value);
        }

        public int TotalListings
        {
            get => accountVModel._totalListings;
            set => Set(ref accountVModel._totalListings, value);
        }

        public int SoldListings
        {
            get => accountVModel._soldListings;
            set => Set(ref accountVModel._soldListings, value);
        }

        public int InActiveListings
        {
            get => accountVModel._inActiveListings;
            set => Set(ref accountVModel._inActiveListings, value);
        }

        public int ActiveUsers
        {
            get => accountVModel._activeUsers;
            set => Set(ref accountVModel._activeUsers, value);
        }

        public int InActiveUsers
        {
            get => accountVModel._inActiveUsers;
            set => Set(ref accountVModel._inActiveUsers, value);
        }

        public decimal CompanyBalance
        {
            get => accountVModel._companyBalance;
            set => Set(ref accountVModel._companyBalance, value);
        }

        #endregion

        #region Methods


        /// <summary>
        /// Closes the window
        /// </summary>
        public void CloseWindow()
        {
            _viewService.CloseWindow("TheAccountWindow");
        }

        /// <summary>
        /// Sets the informations based on the account logged in
        /// </summary>
        public async Task SetAccountInformations()
        {
            var resources = await new ResourcesProcesser().GetAllResourcesByCompanyId(_dataService.account.CompanyId);
            var companyDetails = await new CompanyProcesser().GetCompanyById(_dataService.account.CompanyId);
            
            AccountName = _dataService.account.Username;
            CompanyName = companyDetails.Name;
            CompanyBalance = companyDetails.Balance;
            TotalListings = resources.Count();
            InActiveListings = resources.Where(r => r.Active == false).Count();
        }

        public void ChangePassword()
        {

        }

        public void ChangePicture()
        {

        }

        public void EditCompany()
        {

        }

        public void CloseFirm()
        {

        }
        #endregion
    }
}
