using System;
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

        public Account GetAccount(int id)
        {
            Account returnedAccount = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT user_id, account_id, balance FROM account WHERE user_id = @user_id", conn);
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
        //I can't send more TE Bucks than I have in my account.
        //I can't send a zero or negative amount.
        //A Sending Transfer has an initial status of Approved.
        //public void SendMoney_UpdateReceiversBalance(decimal moneyToTransfer, int toAccountId) // account to void because update??
        //{
        //    //update
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();

        //            SqlCommand cmd = new SqlCommand("UPDATE account SET balance = balance + @moneytotransfer WHERE account_id = @Account_id;", conn);
        //            cmd.Parameters.AddWithValue("@moneytotransfer", moneyToTransfer);
        //            cmd.Parameters.AddWithValue("@Account_id", toAccountId);

        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch (SqlException)
        //    {
        //        throw;
        //    }
        //}

        //public void UpdateSendersBalance(decimal moneyToTransfer, int fromAccountId)
        //{
        //    //update
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();

        //            SqlCommand cmd = new SqlCommand("UPDATE account SET balance = balance - @moneytotransfer WHERE account_id = @Account_id", conn);
        //            cmd.Parameters.AddWithValue("@moneytotransfer", moneyToTransfer);
        //            cmd.Parameters.AddWithValue("@Account_id", fromAccountId);

        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch (SqlException)
        //    {
        //        throw;
        //    }
        //}

        public bool UpdateAccountBalance(Account updatedAccount)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE account SET balance = @balance WHERE user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@balance", updatedAccount.Balance);
                    cmd.Parameters.AddWithValue("@user_id", updatedAccount.UserId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
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

                    SqlCommand cmd = new SqlCommand("SELECT account_id, user_id, balance FROM account", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
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
