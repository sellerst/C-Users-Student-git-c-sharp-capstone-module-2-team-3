﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;
using System.Data.SqlClient;

namespace TenmoServer.DAO
{
    public class AccountSqlDao : IAccountDao
    {
        private readonly string connectionString;
        //const decimal startingBalance = 1000;

        public AccountSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Account GetBalance(int id)
        {
            Account returnedAccount = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT balance FROM account WHERE user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return returnedAccount = GetAccountFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {

                throw;
            }
            return returnedAccount;
        }

        //Send Money
        public void SendMoney(decimal moneyToTransfer, int accountId) // account to void because update??
        {
            //update
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE account SET balance = balance + @moneytotransfer WHERE account_id = @account_id", conn);
                    cmd.Parameters.AddWithValue("@moneytotransfer", moneyToTransfer);
                    cmd.Parameters.AddWithValue("@account_id", accountId);

                    cmd.ExecuteNonQuery(); 
                }
            }
            catch(SqlException)
            {
              throw;
            }
            

        }

        public List<Account> GetListOfUsers()
        {
            List<Account> accounts = new List<Account>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT account_id, user_id FROM account", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Account account = GetAccountFromReader(reader);
                        accounts.Add(account);
                    }
                        
                }

            }
            catch (SqlException)
            {

                throw;
            }
            return accounts;
        }

        private Account GetAccountFromReader(SqlDataReader reader)
        {
            Account u = new Account()
            {
                AccountId = Convert.ToInt32(reader["account_id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                Balance = Convert.ToDecimal(reader["balance"])

            };

            return u;
        }



    }
}
