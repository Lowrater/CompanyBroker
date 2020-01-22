using GalaSoft.MvvmLight;
using InteractWPF.DbConnect;
using InteractWPF.Interfaces;
using InteractWPF.View.Windows;
using InteractWPF.ViewModel;

namespace InteractWPF.Services
{
    public class DataService : ViewModelBase, IDataService
    {

        public MsSQLUserInfo msSQLUserInfo { get; set; }



    }
}
