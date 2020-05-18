using CompanyBroker.Model;
using CompanyBroker_API_Helper.Models;
using System.Collections.ObjectModel;

namespace CompanyBroker.Interfaces
{
    public interface IContentService
    {
        /// <summary>
        /// Removes the element of the list and returns it
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        void RemoveSelectedListIndex(ObservableCollection<string> list, string item);

        void AddSelectedListItem(ObservableCollection<string> list, string item);

    }
}