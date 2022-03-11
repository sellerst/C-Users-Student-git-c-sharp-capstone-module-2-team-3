using System;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoConsoleService : ConsoleService
    {
        /************************************************************
            Print methods
        ************************************************************/
        public void PrintLoginMenu()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("Welcome to TEnmo!");
            Console.WriteLine("1: Login");
            Console.WriteLine("2: Register");
            Console.WriteLine("0: Exit");
            Console.WriteLine("---------");
        }

        public void PrintMainMenu(string username)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine($"Hello, {username}!");
            Console.WriteLine("1: View your current balance");
            Console.WriteLine("2: View your past transfers");
            Console.WriteLine("3: View your pending requests");
            Console.WriteLine("4: Send TE bucks");
            Console.WriteLine("5: Request TE bucks");
            Console.WriteLine("6: Log out");
            Console.WriteLine("0: Exit");
            Console.WriteLine("---------");
        }
        public LoginUser PromptForLogin()
        {
            string username = PromptForString("User name");
            if (String.IsNullOrWhiteSpace(username))
            {
                return null;
            }
            string password = PromptForHiddenString("Password");

            LoginUser loginUser = new LoginUser
            {
                Username = username,
                Password = password
            };
            return loginUser;
        }

        // Add application-specific UI methods here...

        public void ViewAccountBalance(decimal balance)
        {
            Console.Clear();
            Console.WriteLine($"Your current account balance is: ${balance}");
            Console.WriteLine("---------");
            Console.ReadLine();
        }





        //Need to figure out how this works with the API/Serverside of things.
        public void ViewListOfUsers(List<Account> userList, int id)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine($"|-----------------Users--------------------|");
            Console.WriteLine($"|   Id | Username                          |");
            Console.WriteLine($"|------+-----------------------------------|");
            foreach (var item in userList)
            {
                if (id != item.UserId)
                {
                    Console.WriteLine($"|   {item.UserId} | {item.UserId}        |");
                }                
            }
            Console.WriteLine($"|------------------------------------------|");
            Console.WriteLine("---------");
            Console.ReadLine();
        }

        public void ViewAllTransfers(List<Transfer> transferList, int id)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine($"|------------------------------------------|");
            Console.WriteLine($"|Transfers                                 |");
            Console.WriteLine($"|ID           From/To                Amount|");
            Console.WriteLine($"|------------------------------------------|");
            foreach (var item in transferList)
            {
                if (id == item.AccountTo)
                {
                    Console.WriteLine($"|{item.TransferId}     From:{item.AccountFrom}                ${item.Amount}|");
                }
                if (id == item.AccountFrom)
                {
                    Console.WriteLine($"|{item.TransferId}     To:{item.AccountTo}                    ${item.Amount}|");
                }
            }
            Console.WriteLine($"|------------------------------------------|");
            Console.WriteLine("---------");
            Console.ReadLine();
        }

        public void TransfersDetails(Transfer transferDetails)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine($"|------------------------------------------|");
            Console.WriteLine($"|Transfer Details                          |");
            Console.WriteLine($"|------+-----------------------------------|");
            Console.WriteLine($"Id: {transferDetails.TransferId}");
            Console.WriteLine($"From: {transferDetails.AccountFrom}");
            Console.WriteLine($"To: {transferDetails.AccountTo}");
            Console.WriteLine($"Type: {transferDetails.TransferTypeId}");
            Console.WriteLine($"Status: {transferDetails.StatusId}");
            Console.WriteLine($"Amount: {transferDetails.Amount}");
        }

    }
}
