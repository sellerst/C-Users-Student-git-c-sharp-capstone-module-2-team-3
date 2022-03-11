using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDao
    {
        Account GetAccount(int id);

        bool UpdateAccountBalance(Account updatedAccount);

        List<Account> GetListOfUsers();
    }
}
