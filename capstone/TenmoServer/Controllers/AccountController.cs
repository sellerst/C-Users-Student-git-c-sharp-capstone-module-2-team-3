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

        [HttpGet("{id}")]
        public ActionResult<Account> GetBalanceById(int id)
        {
            //int userId = Int32.Parse(User.FindFirst("sub")?.Value);
            Account userAccount = accountDao.GetBalance(id);

            if (userAccount != null)
            {
                return userAccount;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public ActionResult SendMoney_UpdateReceiversBalance(decimal moneyToTransfer, int id)
        {
            
            Account toAccountTransfer = accountDao.GetBalance(id);

            if (toAccountTransfer == null)
            {
                return NotFound();
            }
            else
            {
                accountDao.SendMoney_UpdateReceiversBalance(moneyToTransfer, id);
                return Ok();
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateSendersBalance(decimal moneyToTransfer, int id)
        {
            //int userId = Int32.Parse(User.FindFirst("sub")?.Value);
            Account toAccountTransfer = accountDao.GetBalance(id);

            if (toAccountTransfer == null)
            {
                return NotFound();
            }
            else
            {
                accountDao.UpdateSendersBalance(moneyToTransfer, id);
                return Ok();
            }
        }

    }
}
