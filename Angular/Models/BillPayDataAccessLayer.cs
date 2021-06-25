using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiBankWebsiteA2.Data;
using MiBankWebsiteA2.Models;

namespace Angular
{
    public class BillPayDataAccessLayer
    {
        private readonly MiBankContext _db;

        public BillPayDataAccessLayer(MiBankContext db) {
            _db = db;
        }

        public IEnumerable<BillPay> GetAllBillPays() {
            return _db.BillPays.Include(b => b.Payee.Customer).ToList();
        }

        public BillPay getBillPay(int id) {
            return _db.BillPays.Find(id);
        }

        public int UpdateBillPay(BillPay billpay) {
            _db.Entry(billpay).State = EntityState.Modified;
            _db.SaveChanges();
            return 1;
        }

    }
}
