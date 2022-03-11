using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TenmoServer.DAO;
using TenmoServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountDao accountDao;
        private readonly ITransferDao transferDao;

        public AccountController(IAccountDao _accountDao, ITransferDao _transferDao)
        {
            accountDao = _accountDao;
            transferDao = _transferDao;
        }

        [HttpGet()]
        public List<Account> GetListOfUsers()
        {
            return accountDao.GetListOfUsers();
        }

        //Working 3/10/2022
        [HttpGet("{id}")]
        public ActionResult<Account> GetAccountById(int id)      // ***By userId
        {
            //int userId = Int32.Parse(User.FindFirst("sub")?.Value);
            Account userAccount = accountDao.GetAccount(id);

            if (userAccount != null)
            {
                return userAccount;
            }
            else
            {
                return NotFound();
            }
        }

        //[HttpPut("{id}")]
        //public ActionResult SendMoney_UpdateReceiversBalance(decimal moneyToTransfer, int id)
        //{
            
        //    Account toAccountTransfer = accountDao.GetAccount(id);

        //    if (toAccountTransfer == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        accountDao.SendMoney_UpdateReceiversBalance(moneyToTransfer, id);
        //        return Ok();
        //    }
        //}

        //Working 3/10/2022
        [HttpPut("{id}")]
        public ActionResult UpdateSendersBalance(int id, Account updatedAccount)
        {
            //int userId = Int32.Parse(User.FindFirst("sub")?.Value);
            Account toAccountTransfer = accountDao.GetAccount(id);

            if (toAccountTransfer == null)
            {
                return NotFound();
            }
            else
            {
                accountDao.UpdateAccountBalance(updatedAccount);
                return Ok();
            }
        }

    }
}
