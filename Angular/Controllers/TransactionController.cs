using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiBankWebsiteA2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Angular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionDataAccessLayer _transactionDataAccessLayer;

        public TransactionController(TransactionDataAccessLayer transactionDataAccessLayer)
        {
            _transactionDataAccessLayer = transactionDataAccessLayer;
        }

        [HttpGet]
        [Route("Index")]
        public IEnumerable<Transaction> Get()
        {
            return _transactionDataAccessLayer.GetAllTransactions();
        }
    }
}
