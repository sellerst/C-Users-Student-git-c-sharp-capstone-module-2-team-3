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
        public Account SendMoney(decimal moneyToTransfer, Account accountId)
        {
            return null;
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
