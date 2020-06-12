using CompanyBroker_API_Helper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
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
        public async Task<bool> CreateAccount(AccountAPIModel createAccountAPIModel)
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
        public async Task<AccountModel> VerifyAccount(string userName, string Password)
        {
            //-- Password encoding 
            var usernameConv = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(userName));
            var passwordConv = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(Password));
            //var message = JsonConvert.SerializeObject(new { UserName = loginModel.Username, Password = loginModel.Password });

            //-- API controller url containing the method to add an account
            string url = $"http://localhost:50133/api/VerifyLogin?Username="+ usernameConv + "&Password=" + passwordConv;

             //--Contacting the api with the model of an account
            using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(url))
            {
                //-- Checks if the response code of the post, is successfull
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<AccountModel>();
                }
                else
                {
                    return null;
                }
            }


        }

        /// <summary>
        /// Fetches an account based on username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<AccountModel> FetchAccountByName(string userName, int companyId)
        {
            string url = $"http://localhost:50133/api/Account?userName="+userName+"&companyId="+companyId;

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

        /// <summary>
        /// Updates the account informations
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateAccountInformations(AccountAPIModel AccountAPIModel)
        {
            string url = $"http://localhost:50133/api/Account";

            using (HttpResponseMessage response = await APIHelper.ApiClient.PutAsJsonAsync(url, AccountAPIModel))
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
        /// Deletes the user account
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DeleteUserAccount(int companyId, string username)
        {
            string url = $"http://localhost:50133/api/Account?companyId="+companyId+"&username="+username;

            using (HttpResponseMessage response = await APIHelper.ApiClient.DeleteAsync(url))
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

    }
}
