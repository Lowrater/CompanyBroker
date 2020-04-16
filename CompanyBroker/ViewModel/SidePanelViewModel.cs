using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CompanyBroker.Addons.CustomElements;
using CompanyBroker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CompanyBroker.ViewModel
{
    public class SidePanelViewModel : ViewModelBase
    {
        //------------------------------------------------------------------------------------------------ Interfaces 
        private IDataService dataService;

        //------------------------------------------------------------------------------------------------ ICommands
        public ICommand ShowCustomNameTagCommand => new RelayCommand<Custom_button>(ShowCustomNameTagBtn);

        //------------------------------------------------------------------------------------------------ Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_dataService"></param>
        public SidePanelViewModel(IDataService _dataService)
        {
            this.dataService = _dataService;
        }

        /// <summary>
        /// Method which takes a custom element type of Custom_button as a parameter, from the button which has this ICommand reference.
        /// Displays the custom element string attached to it.
        /// </summary>
        /// <param name="TheTagName"></param>
        public void ShowCustomNameTagBtn(Custom_button TheTagName)
        {
            //-- Messages the user that they are logged in
            MessageBox.Show($"Your custom button says: {TheTagName.CustomButtonNameTag}",
                            "Custom element UI property",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }
    }
}
