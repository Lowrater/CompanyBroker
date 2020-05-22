using CompanyBroker.Interfaces;
using CompanyBroker.Model;
using CompanyBroker_API_Helper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker.Services
{
    public class ContentService : IContentService
    {
        /// <summary>
        /// Removes the element of the list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public void AddSelectedListItem(ObservableCollection<string> list, string item)
        {
            if (!list.Contains(item))
            {
                list.Add(item);
            }
        }

    }
}
