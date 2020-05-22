using CompanyBroker.Model;
using CompanyBroker_API_Helper.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CompanyBroker.Interfaces
{
    public interface IContentService
    {
        void AddSelectedListItem(ObservableCollection<string> list, string item);
    }
}