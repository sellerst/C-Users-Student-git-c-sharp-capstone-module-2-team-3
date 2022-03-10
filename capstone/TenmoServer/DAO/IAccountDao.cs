using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDao
    {
        //See Account Balance
        Account GetBalance(int id);

        //Send Money
        void SendMoney_UpdateReceiversBalance(decimal moneyToTransfer, int AccountId);

        void UpdateSendersBalance(decimal moneyToTransfer, int AccountId);

        List<Account> GetListOfUsers();

    }
}
