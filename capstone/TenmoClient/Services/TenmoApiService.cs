using RestSharp;
using System;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoApiService : AuthenticatedApiService
    {
        public readonly string ApiUrl;

        public TenmoApiService(string apiUrl) : base(apiUrl) { }

        // Add methods to call api here...

        public Account GetAccount(int accountId)
        {
            RestRequest request = new RestRequest($"account/{accountId}");
            IRestResponse<Account> response = client.Get<Account>(request);

            CheckForError(response, $"Get account balance {accountId}");
            return response.Data;
        }
        public List<Transfer> GetAllPastTransfers()
        {
            RestRequest request = new RestRequest($"/account/{UserId}/transfer"); //-------------------NEED THE RIGHT URL???? ******
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);

            CheckForError(response, "Get transfers");
            return response.Data;
        }

        private void CheckForError(IRestResponse response, string action)
        {
            string message = string.Empty;
            string messageDetails = "";
            //Cannot connect to the server.
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                message = $"Error occurred in '{action}' - unable to reach server.";
                messageDetails = $"Action: {action} {Environment.NewLine}" +
                    $"\tResponse status was '{response.ResponseStatus}'.";
                if (response.ErrorException != null)
                {
                    messageDetails += $"{Environment.NewLine}\t{response.ErrorException.Message}";
                }
            } //Connected, got a response, but something went wrong. 
            else if (!response.IsSuccessful)
            {
                message = "An http error occurred.";
                messageDetails = $"Action '{action}'{Environment.NewLine}" +
                    $"\tResponse: {(int)response.StatusCode} {response.StatusDescription}";
            }


        }

    }
}
