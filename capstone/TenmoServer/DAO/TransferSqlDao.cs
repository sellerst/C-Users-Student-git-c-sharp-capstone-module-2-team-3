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
