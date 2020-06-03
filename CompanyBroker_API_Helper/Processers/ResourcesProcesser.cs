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
        #region CompanyResources Tabel
        //-------------------------------------------------- To fetch, update or delete an product
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
                    return new ObservableCollection<ResourcesModel>();
                }
            }
        }

        /// <summary>
        /// Adds a new resource to the database
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AddNewResources(ResourcesModel resources)
        {
            //-- URL path to the resources 
            var url = $"http://localhost:50133/api/Resources";
            //-- Sends an requests to fetch the data
            using (HttpResponseMessage response = await APIHelper.ApiClient.PostAsJsonAsync(url, resources))
            {
                //-- verifys the response
                if (response.IsSuccessStatusCode)
                {
                    //-- returns the content
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Fetches an product based on CompanyId and productName
        /// </summary>
        /// <returns></returns>
        public async Task<ResourcesModel> GetResourceByCompanyIdAndName(int companyId, string ProductName)
        {
            //-- URL path to the resources 
            var url = $"http://localhost:50133/api/Resources?companyId="+companyId+"&productName="+ProductName;
            //-- Sends an requests to fetch the data
            using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(url))
            {
                //-- verifys the response
                if (response.IsSuccessStatusCode)
                {
                    //-- returns the content
                    return await response.Content.ReadAsAsync<ResourcesModel>();
                }
                else
                {
                    return null;
                }
            }
        }
        


        /// <summary>
        /// returns list of resources based on a searched word
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
                    return new ObservableCollection<ResourcesModel>();
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
                    return new ObservableCollection<ResourcesModel>();
                }
            }
        }

        /// <summary>
        /// Fetches all resources for the specific company
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task<ObservableCollection<ResourcesModel>> GetAllResourcesByCompanyId(int companyId)
        {
            var url = $"http://localhost:50133/api/GetResourcesByCompanyId?companyId=" + companyId;

            using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(url))
            {
                if(response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<ObservableCollection<ResourcesModel>>();
                }
                else
                {
                    return new ObservableCollection<ResourcesModel>();
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
                    return new ObservableCollection<string>();
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
                    return new ObservableCollection<string>();
                }
            }
        }


        /// <summary>
        /// Updates an specific product
        /// </summary>
        /// <param name="resourcesModel"></param>
        /// <returns></returns>
        public async Task<bool> UpdateResourceInformations(ResourcesModel resourcesModel)
        {
            var url = $"http://localhost:50133/api/Resources";

            using (HttpResponseMessage response = await APIHelper.ApiClient.PutAsJsonAsync(url, resourcesModel))
            {
                if(response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        /// <summary>
        /// Changes the resource amount by increasing or decreasing
        /// </summary>
        /// <param name="resourceAmountChangeModel"></param>
        /// <returns></returns>
        public async Task<bool> ChangeCompanyResourceAmount(int companyId, int resourceId, bool increaseAmount)
        {
            var url = $"http://localhost:50133/api/ChangeCompanyResourceAmount";

            var resourceChange = new ResourceChangeAmountModel()
            {
                companyId = companyId,
                resourceId = resourceId,
                IncreaseAmount = increaseAmount
            };

            using (HttpResponseMessage response = await APIHelper.ApiClient.PutAsJsonAsync(url, resourceChange))
            {
                if(response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Deletes an product for the company based on companyId, productname and resourceId
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="productName"></param>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteProduct(int companyId, string productName, int resourceId)
        {
            var url = $"http://localhost:50133/api/Resources?companyId="+companyId+"&productName="+productName+"&resourceId="+resourceId;
            using (HttpResponseMessage response = await APIHelper.ApiClient.DeleteAsync(url))
            {
                if(response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion



        #region ResourcesDescription Table
        //-------------------------------------------------- To fetch, put or update an product description
        /// <summary>
        /// Adds a new product description to the database
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task<bool> AddProductDescription(string description, int resourceId)
        {
            var url = $"http://localhost:50133/api/ResourceDescription";

            var resDescr = new ResourceDescriptionModel()
            {
                Description = description,
                ResourceId = resourceId
            };

            using (HttpResponseMessage response = await APIHelper.ApiClient.PostAsJsonAsync(url, resDescr)) 
            {
                if(response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Adds a new product description to the database
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task<bool> UpdateProductDescription(string description, int resourceId)
        {
            var url = $"http://localhost:50133/api/ResourceDescription";

            var resdesc = new ResourceDescriptionModel()
            {
                Description = description,
                ResourceId = resourceId
            };

            using (HttpResponseMessage response = await APIHelper.ApiClient.PutAsJsonAsync(url, resdesc))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets the description of the object
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task<ResourceDescriptionModel> GetResourceDescription(int resourceId)
        {
            var url = $"http://localhost:50133/api/ResourceDescription?resourceId="+ resourceId;

            using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<ResourceDescriptionModel>();
                }
                else
                {
                    return null;
                }
            }
        }


        #endregion
    }
}
