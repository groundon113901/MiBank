using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiBankWebsiteA2.Data;
using MiBankWebsiteA2.Models;

namespace Angular
{
    public class LoginDataAccessLayer
    {
        private readonly MiBankContext _db;

        public LoginDataAccessLayer(MiBankContext db) {
            _db = db;
        }

        public IEnumerable<Login> GetAllLogins() {
            return _db.Logins.Include(l => l.Customer).ToList();
        }

        public Login getLoginById(int id) {
            return _db.Logins.Find(id);
        }

        public int UpdateLogin(Login login) {
            _db.Entry(login).State = EntityState.Modified;
            _db.SaveChanges();
            return 1;
        }
    }
}
