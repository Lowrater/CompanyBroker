using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
      

        //------------------------------------------------------------------------------------------------ Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_dataService"></param>
        public SidePanelViewModel(IDataService _dataService)
        {
            this.dataService = _dataService;
        }


    }
}
