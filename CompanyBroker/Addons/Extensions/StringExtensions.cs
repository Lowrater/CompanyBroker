using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker.Addons.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts string to int, if it contains an number
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int ConvertToInt(this string text)
        {
            return Int32.Parse(text);
        }
    }
}
