using RestSharp;
using System;
using System.Collections.Generic;
using TenmoClient.Models;
using System.Net.Http;

namespace TenmoClient.Services
{
    public class TenmoApiService : AuthenticatedApiService
    {
        public readonly string ApiUrl;
        private static Account user = new Account();

        public TenmoApiService(string apiUrl) : base(apiUrl) { }

        public int AccountID
        {
            get
            {
                return (user == null) ? 0 : user.AccountId;
            }
        }

        public decimal Balance 
        { 
            get 
            {
                return (user == null) ? 0 : user.Balance;
            } 
            set 
            { 
            
            } 
        }





        //public int AccountId { get; set; }
        //public int UserId { get; set; }
        //public decimal Balance { get; set; }






        // Add methods to call api here...

        public List<Account> GetAccounts()
        {
            RestRequest request = new RestRequest("account");
            IRestResponse<List<Account>> response = client.Get<List<Account>>(request);
            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"There was an error in the call to the server {response.StatusCode}");
            }
            return response.Data;
        }

        public Account GetAccount(int userId)
        {
            RestRequest request = new RestRequest($"account/{userId}");
            IRestResponse<Account> response = client.Get<Account>(request);
            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"There was an error in the call to the server {response.StatusCode}");
            }
            return response.Data;
        }

        public List<Transfer> GetAllTransfer(int userId)
        {
            RestRequest request = new RestRequest($"account/{userId}/transfer");
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);
            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"There was an error in the call to the server {response.StatusCode}");
            }
            return response.Data;
        }

        public Transfer GetTransfer(int transferId)
        {
            RestRequest request = new RestRequest($"transfer/{transferId}");
            IRestResponse<Transfer> response = client.Get<Transfer>(request);
            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"There was an error in the call to the server {response.StatusCode}");
            }
            return response.Data;
        }

        public bool CheckTransfer(int transferId)
        {
            RestRequest request = new RestRequest($"transfer/{transferId}");
            IRestResponse<Transfer> response = client.Get<Transfer>(request);
            if (!response.IsSuccessful)
            {
                return false;
            }
            return true;
        }
    }
}
