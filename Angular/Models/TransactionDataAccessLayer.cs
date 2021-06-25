
using MiBankWebsiteA2.Data;
using MiBankWebsiteA2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular
{
    public class TransactionDataAccessLayer
    {
        private readonly MiBankContext _db;
        public TransactionDataAccessLayer(MiBankContext db)
        {
            _db = db;
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _db.Transactions.Include(t => t.Account.Customer).ToList();
        }
    }
}
