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

        Transfer CreateTransfer(Transfer newTransfer);

        //Step 7-9(If we have time)
        void UpdateTransfer(int decision, int transferId);

    }
}
