using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDao
    {
        //See All Transfers
        List<Transfer> GetAllTransfers(int Id);

        //See specific Transfer by ID
        Transfer GetTransferById(int Id);

        Transfer CreateTransfer(decimal amount, int accountFrom, int accountTo);

        void UpdateTransfer(int decision, int transferId);

    }
}
