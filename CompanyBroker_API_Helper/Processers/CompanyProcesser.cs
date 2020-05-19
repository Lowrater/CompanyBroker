using CompanyBroker_API_Helper.Models;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace CompanyBroker_API_Helper.Processers
{
    /// <summary>
    /// Class containing all informations regarding a company
    /// </summary>
    public class CompanyProcesser
    {
        /// <summary>
        /// Returns all companies without balance informations
        /// </summary>
        /// <returns></returns>
        public async Task<ObservableCollection<CompanyModel>> GetAllCompanies()
        {
            //-- The API url
            string url = $"http://localhost:50133/api/Companies";
            //-- Opens up a new Http request from the ApiClient created with the url path.
            using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(url))
            {
                //-- If it's successfull, we can do something with the returned data.
                if (response.IsSuccessStatusCode)
                {
                    //-- Returns the results
                    return await response.Content.ReadAsAsync<ObservableCollection<CompanyModel>>();
                }
                else
                {
                    return new ObservableCollection<CompanyModel>();
                }
            }
        }

        /// <summary>
        /// Get's a person or returns the person lists.
        /// Is Async due we are requesting something on the WWW, and don't want to lock the user
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public async Task<CompanyModel> GetCompany(int companyId)
        {
            //-- Sets new Url
            string url = $"http://localhost:50133/api/Companies/" + $"{companyId}";

            //-- Checks wether or not the number is zero, if not then we return the whole list
            if (companyId > 0)
            {
                //-- Opens up a new Http request from the ApiClient created with the url path.
                using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(url))
                {
                    //-- If it's successfull, we can do something with the returned data.
                    if (response.IsSuccessStatusCode)
                    {
                        //-- Awaits the response of the content requested in async, but it must convert into a CompanyModel
                        //-- Here it will match it's content, to the properties stored in the CompanyModel object.
                        return await response.Content.ReadAsAsync<CompanyModel>();
                    }
                    else
                    {
                        return new CompanyModel();
                    }
                }
            }
            else
            {
                return new CompanyModel();
            }
        }

        /// Get's a person or returns the person lists.
        /// Is Async due we are requesting something on the WWW, and don't want to lock the user
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public async Task<CompanyModel> GetCompany(string companyName)
        {
            //-- Sets new Url
            string url = $"http://localhost:50133/api/Companies?companyName="+$"{companyName}";

            //-- Checks wether or not the number is zero, if not then we return the whole list
            if (!string.IsNullOrEmpty(companyName))
            {
                //-- Opens up a new Http request from the ApiClient created with the url path.
                using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(url))
                {
                    //-- If it's successfull, we can do something with the returned data.
                    if (response.IsSuccessStatusCode)
                    {
                        //-- Awaits the response of the content requested in async, but it must convert into a CompanyModel
                        //-- Here it will match it's content, to the properties stored in the CompanyModel object.
                        return await response.Content.ReadAsAsync<CompanyModel>();
                    }
                    else
                    {
                        return new CompanyModel();
                    }
                }
            }
            else
            {
                return new CompanyModel();
            }
        }

        /// <summary>
        /// Creates an new company
        /// </summary>
        /// <param name="createCompanyAPIModel"></param>
        /// <returns></returns>
        public async Task<bool> CreateCompany(CreateCompanyAPIModel createCompanyAPIModel)
        {
            //-- Bool state to return
            bool state = false;
            //-- API controller url containing the method to add an account
            string url = $"http://localhost:50133/api/CreateCompany";
            //-- Contacting the api with the model of an account
            using (HttpResponseMessage response = await APIHelper.ApiClient.PostAsJsonAsync(url, createCompanyAPIModel))
            {
                //-- Checks if the response code of the post, is successfull
                if (response.IsSuccessStatusCode)
                {
                    //-- sets the state
                    state = true;
                }
            }
            //-- returns the state
            return state;
        }
    }
}
