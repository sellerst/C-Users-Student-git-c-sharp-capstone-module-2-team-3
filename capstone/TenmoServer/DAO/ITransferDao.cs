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
        List<Transfer> SeeAllTransfer();

        //See specific Transfer by ID
        Transfer SeeTransferById(int Id);


    }
}
