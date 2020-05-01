using CompanyBroker.Interfaces;
using CompanyBroker.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyBroker.ViewModel
{
    /// <summary>
    /// Used for BrokerOverviewControl, SearchBarControl
    /// Uses filters from SidePanelTab1
    /// </summary>
   public class BrokerOverviewViewModel : ViewModelBase 
    {
        //---------------------------------------------------------------- Model
       private BrokerOverviewModel brokerOverviewModel = new BrokerOverviewModel();
        //---------------------------------------------------------------- Interfaces
        private IDBService _dBService;
        private IDataService _dataservice;
        private IAppConfigService _appConfigService;
        private IContentService _contentService;
        //---------------------------------------------------------------- ICommands
        public ICommand ExecuteQueryCommand => new RelayCommand(async () =>  MainTable = await FillDataTable());

        //---------------------------------------------------------------- Constructor
        public BrokerOverviewViewModel(IDBService __dBService, IDataService __dataservice, IAppConfigService __appConfigService, IContentService __contentService)
        {
            this._dBService = __dBService;
            this._dataservice = __dataservice;
            this._appConfigService = __appConfigService;
            this._contentService = __contentService;
        }
        //---------------------------------------------------------------- Properties

        /// <summary>
        /// The main table which contains the table of every query contents
        /// </summary>
        public DataTable MainTable
        {
            get => brokerOverviewModel._mainTable;
            set => Set(ref brokerOverviewModel._mainTable, value);
        }
        //---------------------------------------------------------------- Methods

        /// <summary>
        /// Fills the table for the user, depending on the filters provided from the user in the SidePanelTab1ViewModel
        /// </summary>
        public async Task<DataTable> FillDataTable()
        {
            using (var dbconnection = new SqlConnection(_appConfigService.SQL_connectionString))
            {
                return await _dBService.ExecuteQuery(dbconnection,"select * from CompanyResources");
            }
        }
    }
}
