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

        public void PrintBalance(Account account)
        {
            Console.WriteLine($"Your current account balance is: {account.Balance}");
        }

        public void PrintAllPastTransfers(List<Transfer> transfers)
        {
            Console.WriteLine("-------------------------------------------"); 
            Console.WriteLine("Transfers");
            Console.WriteLine("ID               From/To             Amount");
            Console.WriteLine("-------------------------------------------");
            if (transfers.Count > 0)
            {
                foreach (Transfer transfer in transfers)
                {
                    Console.WriteLine($"{transfer.TransferId}               {transfer.TransferTypeId}               {transfer.Amount}");
                }
            }
            else
            {
                Console.WriteLine("You have no transfers!");
            }
        }

    }
}
