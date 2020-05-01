using CompanyBroker.Interfaces;
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
        /// Removes the element of the list and returns it
        /// </summary>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public void RemoveSelectedListIndex(ObservableCollection<string> list, string item)
        {
            list.Remove(item);
            //return list;
        }


        /// <summary>
        /// Removes the element of the list and returns it
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

        /// <summary>
        /// Appends all items from an list, into a string
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string SQLContentParameterAppends(ObservableCollection<string> list)
        {
            //-- string to append all choosen product types
            var stringContentParameters = new StringBuilder();

            //-- Adds the choosen product types to the string builder.
            foreach (var item in list)
            {
                stringContentParameters.Append($"'{item}'" + ", ");
            }

            //-- Combiens everything together
            string newCommand = "( " + stringContentParameters.ToString().Remove(stringContentParameters.ToString().Length - 2).ToString() + ")";

            //-- returns the result
            return newCommand;
        }
    }
}
