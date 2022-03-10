using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;
using System.Data.SqlClient;

namespace TenmoServer.DAO
{
    public class TransferSqlDao : ITransferDao
    {
        private readonly string connectionString;
        //const decimal startingBalance = 1000;

        public TransferSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Transfer> GetAllTransfers(int accountId)
        {
            List<Transfer> listOfAllTransfers = new List<Transfer>();
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(@"SELECT * FROM transfer WHERE account_from = @accountId OR account_to = @accountId", conn);
                    command.Parameters.AddWithValue("@accountId", accountId);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Transfer transfer = GetTransferFromReader(reader);
                        listOfAllTransfers.Add(transfer);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            
            return listOfAllTransfers;
        }

        public Transfer GetTransferById (int transferId)
        {
            Transfer transfer = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM transfer WHERE transfer_id = @transferId", conn);
                cmd.Parameters.AddWithValue("@transferId", transferId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    transfer = GetTransferFromReader(reader);
                }
            }
            return transfer;
        }

        public Transfer CreateTransfer(decimal amount, int accountFrom, int accountTo)
        {
            int newTransferId;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"INSERT INTO transfer(transfer_type_id, transfer_status_id, account_from, account_to, amount)
                                                    OUTPUT INSERTED.transfer_id
                                                     VALUES(2, 1, @account_from, @account_to, @amount)", conn);
                    cmd.Parameters.AddWithValue("@account_from", accountFrom);
                    cmd.Parameters.AddWithValue("@account_to", accountTo);
                    cmd.Parameters.AddWithValue("@amount", amount);

                    newTransferId = Convert.ToInt32(cmd.ExecuteScalar());

                }
            }
            catch (SqlException)
            {
                throw;
            }
            return GetTransferById(newTransferId);

        }

        public void UpdateTransfer(int decision, int transferId)
        {
            int userDecision;
            try
            {
                if (decision == 1)
                {
                    userDecision = 2;
                }
                else
                {
                    userDecision = 3;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE transfer SET transfer_status_id = @user_decision WHERE transfer_id = @transfer_id", conn);
                    cmd.Parameters.AddWithValue("@user_decision", userDecision);
                    cmd.Parameters.AddWithValue("@tranfer_id", transferId);

                    cmd.ExecuteNonQuery();
                }


            }
            catch (SqlException)
            {

                throw;
            }
        }

        private Transfer GetTransferFromReader(SqlDataReader reader)
        {
            Transfer u = new Transfer()
            {
                TransferId = Convert.ToInt32(reader["transfer_id"]),
                TransferTypeId = Convert.ToInt32(reader["transfer_type_id"]),
                StatusId = Convert.ToInt32(reader["transfer_status_id"]),
                AccountFrom = Convert.ToInt32(reader["account_from"]),
                AccountTo = Convert.ToInt32(reader["account_to"]),
                Amount = Convert.ToDecimal(reader["amount"])
                

            };

            return u;
        }
    }
}
