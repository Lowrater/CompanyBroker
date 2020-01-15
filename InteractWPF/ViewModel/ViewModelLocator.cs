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
using InteractWPF.Interfaces;
using InteractWPF.Services;
using System;

namespace InteractWPF.ViewModel
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

            //-- ViewModel registers
            SimpleIoc.Default.Register<LoginViewModel>();
        }

        //-- ViewModel definitions (made public to all XML's)
        // use the following in the XAML to get access to any viewmodel. etc. Login: DataContext="{Binding Login, Source={StaticResource Locator}}
        public LoginViewModel Login => ServiceLocator.Current.GetInstance<LoginViewModel>(Guid.NewGuid().ToString());
         
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}