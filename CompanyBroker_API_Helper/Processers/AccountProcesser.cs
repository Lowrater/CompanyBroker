using CompanyBroker_API_Helper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBroker_API_Helper
{
    /// <summary>
    /// All methods to handle account data
    /// </summary>
    public class AccountProcessor
    {
        /// <summary>
        /// Creates an new account to an existing company
        /// </summary>
        /// <param name="createAccountAPIModel"></param>
        /// <returns></returns>
        public async Task<bool> CreateAccount(CreateAccountAPIModel createAccountAPIModel)
        {
            //-- Bool state to return
            bool state = false;
            //-- API controller url containing the method to add an account
            string url = $"http://localhost:50133/api/CreateAccount";
            //-- Contacting the api with the model of an account
            using (HttpResponseMessage response = await APIHelper.ApiClient.PostAsJsonAsync(url, createAccountAPIModel))
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

        /// <summary>
        /// verifys the account agenst the database
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        public async Task<bool> VerifyAccount(LoginAPIModel loginModel)
        {
            //-- Bool state to return
            bool state = false;
            //-- API controller url containing the method to add an account
            string url = $"http://localhost:50133/api/VerifyLogin";
            //-- Contacting the api with the model of an account
            using (HttpResponseMessage response = await APIHelper.ApiClient.PostAsJsonAsync(url, loginModel))
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

        /// <summary>
        /// Fetches an account based on username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<AccountModel> FetchAccountByName(string userName)
        {
            string url = $"http://localhost:50133/api/Account?userName="+userName;

            using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<AccountModel>();
                }
                else
                {
                    return new AccountModel();
                }
            }
        }

        /// <summary>
        /// Returns an list 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task<ObservableCollection<AccountModel>> FetchAccountsByCompanyId(int companyId)
        {
            string url = $"http://localhost:50133/api/Account?companyId="+ companyId;

            using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(url))
            {
                if(response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<ObservableCollection<AccountModel>>();
                }
                else
                {
                    return new ObservableCollection<AccountModel>();
                }
            }
        }
    
    }
}
