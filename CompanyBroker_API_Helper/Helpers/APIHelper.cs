using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker_API_Helper
{
    /// <summary>
    /// API helper class, that works with the API client itself.
    /// </summary>
   public static class APIHelper
    {
        /// <summary>
        /// The api client
        /// </summary>
        public static HttpClient ApiClient { get; set; }

        /// <summary>
        /// Initializing the apiclient to be used
        /// </summary>
        public static void InitializeApiClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
