using CompanyBroker.Interfaces;
using CompanyBroker.Model.Informations;
using CompanyBroker_API_Helper;
using CompanyBroker_API_Helper.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyBroker.ViewModel.Informations
{
    public class AccountInfoViewModel : ViewModelBase
    {
        #region Model
        private AccountInfoModel accountInfoModel = new AccountInfoModel();
        #endregion

        #region Interfaces
        public ICommand CloseWindowCommand => new RelayCommand(CloseWinodw);
        public ICommand UpdateCommand => new RelayCommand<PasswordBox>(async (p) => await updateInformations(p.Password));
        #endregion

        #region Icommands
        private IDataService _dataService;
        private IViewService _viewService;
        #endregion

        #region Construcor
        public AccountInfoViewModel(IDataService __dataService, IViewService __viewService)
        {
            this._dataService = __dataService;
            this._viewService = __viewService;

            new Action(async () => await SetInformations())();
        }
        #endregion

        #region Properties
        public string AccountName 
        {
            get => accountInfoModel._accountName;
            set => Set(ref accountInfoModel._accountName, value);
        }
        public string AccountEmail
        {
            get => accountInfoModel._accountEmail;
            set => Set(ref accountInfoModel._accountEmail, value);
        }
        public bool IsActive 
        {
            get => accountInfoModel._isActive;
            set => Set(ref accountInfoModel._isActive, value);
        }

        private AccountModel UserAccount { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the informations of the account
        /// </summary>
        /// <returns></returns>
        private async Task SetInformations()
        {
            try
            {
                UserAccount = await new AccountProcessor().FetchAccountByName(_dataService.accountSelection.Username, _dataService.accountSelection.CompanyId);

                AccountName = UserAccount.Username;
                AccountEmail = UserAccount.Email;
                IsActive = UserAccount.Active;
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.ToString().Substring(0,252)}",
                              "Company Broker login error",
                             MessageBoxButton.OK,
                             MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Saves the informations in PUT agens the database
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task updateInformations(string password)
        {
            var UpdatedAccount = new AccountAPIModel()
            {
                Active = IsActive,
                CompanyId = UserAccount.CompanyId,
                Email = AccountEmail,
                Username = AccountName,
                Password = string.Empty
            };

            if (!string.IsNullOrEmpty(password))
            {
                UpdatedAccount.Password = password;
            }

            try
            {
                var accountCheck = await new AccountProcessor().UpdateAccountInformations(UpdatedAccount);

                if(accountCheck != false)
                {
                    MessageBox.Show($"Succesfully updated account informations",
                                      "Company Broker update account",
                                     MessageBoxButton.OK,
                                     MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Failed updating account informations",
                                      "Company Broker update account",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.ToString().Substring(0, 252)}",
                              "Company Broker update account error",
                             MessageBoxButton.OK,
                             MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Closes the window
        /// </summary>
        private void CloseWinodw()
        {
            _viewService.CloseWindow("TheUpdateAccountInfoWindow");
        }
        #endregion
    }
}
