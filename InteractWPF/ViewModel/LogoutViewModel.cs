using GalaSoft.MvvmLight;
using InteractWPF.Interfaces;
using InteractWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractWPF.ViewModel
{
   public class LogoutViewModel : ViewModelBase
    {
       private LogoutModel logoutModel = new LogoutModel();

        //-- Interfaces
        private IDataService dataService;

        /// <summary>
        /// COnstructor
        /// </summary>
        public LogoutViewModel(IDataService _dataService)
        {
            this.dataService = _dataService;
            
        }

        //-- properties to the view 
        public string currentDateTime
        {
            get => logoutModel._currentdatetime;
            set => Set(ref logoutModel._currentdatetime, value);
        }
    }
}
