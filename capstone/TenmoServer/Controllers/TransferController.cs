﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly IAccountDao accountDao;
        private readonly ITransferDao transferDao;

        public TransferController(IAccountDao _accountDao, ITransferDao _transferDao)
        {
            accountDao = _accountDao;
            transferDao = _transferDao;
        }

        [HttpGet("/{id}/transfer")] //must determine url addy
        public ActionResult<List<Transfer>> GetAllTransfersByUser(int id)
        {
           Account refAccount = accountDao.GetBalance(id);
            if(refAccount == null)
            {
                return NotFound();
            }
            return transferDao.GetAllTransfers(id);
        }

        [HttpGet("{id}")]
        public ActionResult<Transfer> GetSpecificTransferById(int id)
        {
            Transfer transfer = transferDao.GetTransferById(id);

            if (transfer != null)
            {
                return transfer;
            }
            else
            {
                return NotFound();
            }
        }



    }
}
