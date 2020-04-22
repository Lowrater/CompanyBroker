using CompanyBroker.Interfaces;
using CompanyBroker.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker.ViewModel
{
   public class TimeViewModel : ViewModelBase
    {
        //-- Models
        private TimeModel timeModel = new TimeModel();

        //-- Interfaces
        private IDataService _dataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public TimeViewModel(IDataService dataService)
        {
            //-- Dependency injection
            _dataService = dataService;

            //-- Set's the date as first startup
            currentDateTime = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();

            //--- Calling async method in contructor
            //--- Sets the date every secod as long as we are connected.
            new Action(async () => await SetTime())();
        }

        //-- properties to the view 
        /// <summary>
        /// Time date
        /// </summary>
        public string currentDateTime
        {
            get => timeModel._time;
            set => Set(ref timeModel._time, value);
        }

        /// <summary>
        /// Sets the date every 10 seconds as long as the user is connected.
        /// </summary>
        /// <returns></returns>
        private async Task SetTime()
        {
            //-- While the user is active, update the timestamp
            while (_dataService.isConnected.Equals(true))
            {
                //-- waits 30 seconds 
                await Task.Delay(10000);
                //-- sets the currentDateTime property 
                currentDateTime = await Task.FromResult<string>(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString());
            }
        }
    }
}
