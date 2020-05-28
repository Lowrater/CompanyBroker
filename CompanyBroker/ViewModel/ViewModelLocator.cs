/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:InteractWPF"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using CompanyBroker.Interfaces;
using CompanyBroker.Services;
using System;
using CompanyBroker.ViewModel.AccountControl;
using CompanyBroker.ViewModel.Informations;

namespace CompanyBroker.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //--  Services registers
            SimpleIoc.Default.Register<IDataService, DataService>();
            SimpleIoc.Default.Register<IViewService, ViewService>();
            SimpleIoc.Default.Register<IAppConfigService, AppConfigService>();
            SimpleIoc.Default.Register<IContentService, ContentService>();


            //-- ViewModel registers
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<LogoutViewModel>();
            SimpleIoc.Default.Register<SidePanelViewModel>();
            SimpleIoc.Default.Register<TimeViewModel>();
            SimpleIoc.Default.Register<SidePanelTab1ViewModel>();
            SimpleIoc.Default.Register<CreateAccountViewModel>();
            SimpleIoc.Default.Register<BrokerOverviewViewModel>();
            SimpleIoc.Default.Register<AccountViewModel>();
            SimpleIoc.Default.Register<FirmAccountsViewModel>();
            SimpleIoc.Default.Register<FirmFavoritsViewModel>();
            SimpleIoc.Default.Register<FirmListingViewModel>();
            SimpleIoc.Default.Register<FirmPartnersViewModel>();
            SimpleIoc.Default.Register<CompanyAddListingViewModel>();
            SimpleIoc.Default.Register<ResourceInfoViewModel>();
            SimpleIoc.Default.Register<AccountInfoViewModel>();
        }

        //-- ViewModel definitions (made public to all XML's)
        // use the following in the XAML to get access to any viewmodel. etc. Login: DataContext="{Binding Login, Source={StaticResource Locator}}
        public LoginViewModel Login => ServiceLocator.Current.GetInstance<LoginViewModel>(Guid.NewGuid().ToString());
        public LogoutViewModel Logout => ServiceLocator.Current.GetInstance<LogoutViewModel>(Guid.NewGuid().ToString());
        public SidePanelViewModel Sidepanel => ServiceLocator.Current.GetInstance<SidePanelViewModel>(Guid.NewGuid().ToString());
        public TimeViewModel Time => ServiceLocator.Current.GetInstance<TimeViewModel>(Guid.NewGuid().ToString());
        public SidePanelTab1ViewModel SidePanelTab1 => ServiceLocator.Current.GetInstance<SidePanelTab1ViewModel>(Guid.NewGuid().ToString());
        public CreateAccountViewModel CreateAccount => ServiceLocator.Current.GetInstance<CreateAccountViewModel>(Guid.NewGuid().ToString());
        public BrokerOverviewViewModel BrokerOverview => ServiceLocator.Current.GetInstance<BrokerOverviewViewModel>(Guid.NewGuid().ToString());
        public AccountViewModel Account => ServiceLocator.Current.GetInstance<AccountViewModel>(Guid.NewGuid().ToString());
        public FirmAccountsViewModel FirmAccount => ServiceLocator.Current.GetInstance<FirmAccountsViewModel>(Guid.NewGuid().ToString());
        public FirmFavoritsViewModel FirmFavorit => ServiceLocator.Current.GetInstance<FirmFavoritsViewModel>(Guid.NewGuid().ToString());
        public FirmListingViewModel FirmListing => ServiceLocator.Current.GetInstance<FirmListingViewModel>(Guid.NewGuid().ToString());
        public FirmPartnersViewModel FirmPartners => ServiceLocator.Current.GetInstance<FirmPartnersViewModel>(Guid.NewGuid().ToString());
        public CompanyAddListingViewModel CompanyAddListing => ServiceLocator.Current.GetInstance<CompanyAddListingViewModel>(Guid.NewGuid().ToString());
        public ResourceInfoViewModel ResourceInfo => ServiceLocator.Current.GetInstance<ResourceInfoViewModel>(Guid.NewGuid().ToString());
        public AccountInfoViewModel AccountInfo => ServiceLocator.Current.GetInstance<AccountInfoViewModel>(Guid.NewGuid().ToString());


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}