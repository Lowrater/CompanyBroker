using CompanyBroker_API_Helper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker_API_Helper.Processers
{
    public class ResourcesProcesser
    {
        /// <summary>
        /// Fetches all resources
        /// </summary>
        /// <returns></returns>
        public async Task<ObservableCollection<ResourcesModel>> GetAllResources()
        {
            //-- URL path to the resources 
            var url = $"http://localhost:50133/api/Resources";
            //-- Sends an requests to fetch the data
            using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(url))
            {
                //-- verifys the response
                if(response.IsSuccessStatusCode)
                {
                    //-- returns the content
                    return await response.Content.ReadAsAsync<ObservableCollection<ResourcesModel>>();
                }
                else
                {
                    //-- Throws an exception if it's not successful
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchWord"></param>
        /// <returns></returns>
        public async Task<ObservableCollection<ResourcesModel>> GetResourcesBySearch(string searchWord)
        {
            var url = $"http://localhost:50133/api/Resources?searchWord="+ searchWord;

            using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(url))
            {
                if(response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<ObservableCollection<ResourcesModel>>();
                }
                else
                {
                    //-- Throws an exception if it's not successful
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        /// <summary>
        /// Fetches all resources based on the collectionListModel lists
        /// </summary>
        /// <param name="collectionFilterModel"></param>
        /// <returns></returns>
        public async Task<ObservableCollection<ResourcesModel>> GetResourcesByListFilters(CollectionFilterModel collectionFilterModel)
        {
            var url = $"http://localhost:50133/api/GetResourcesByListFilters";

            using (HttpResponseMessage response = await APIHelper.ApiClient.PostAsJsonAsync(url, collectionFilterModel))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<ObservableCollection<ResourcesModel>>();
                }
                else
                {
                    //-- Throws an exception if it's not successful
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        /// <summary>
        /// Fetches all product types
        /// </summary>
        /// <returns></returns>
        public async Task<ObservableCollection<string>> GetAllProductTypes()
        {
            var url = $"http://localhost:50133/api/GetProductAllTypes";

            using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(url))
            {
                if(response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<ObservableCollection<string>>();
                }
                else
                {
                    //-- Throws an exception if it's not successful
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        /// <summary>
        /// Fetches all product names by type
        /// </summary>
        /// <param name="productTypeList"></param>
        /// <returns></returns>
        public async Task<ObservableCollection<string>> GetAllProductNamesByType(ObservableCollection<string> productTypeList)
        {
            var url = $"http://localhost:50133/api/GetAllProductNamesByTypes";

            //HttpContent content =  new StringContent(JsonConvert.SerializeObject(productTypeList), Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await APIHelper.ApiClient.PostAsJsonAsync(url, productTypeList))
            {
                if(response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<ObservableCollection<string>>();
                }
                else
                {
                    //-- Throws an exception if it's not successful
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


    }
}
