using GalaSoft.MvvmLight;
using InteractWPF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractWPF.ViewModel
{
    public class SidePanelViewModel : ViewModelBase
    {
        //-- Interfaces 
        private IDataService dataService;

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
