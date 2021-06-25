using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiBankWebsiteA2.Data;
using MiBankWebsiteA2.Models;

namespace Angular
{
    public class CustomerDataAccessLayer
    {
        private readonly MiBankContext _db;

        public CustomerDataAccessLayer(MiBankContext db)
        {
            _db = db;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var customers = _db.Customers.Include(c => c.Accounts).ToList();
            return customers;
        }

        // To Update the records of a particular customer.
        public int UpdateCustomer(Customer customer)
        {
            _db.Entry(customer).State = EntityState.Modified;
            _db.SaveChanges();
            return 1;
        }

        // Get the details of a particular customer.
        public Customer GetCustomerData(int id)
        {
            var customer = _db.Customers.Find(id);
            return customer;
        }
    }
}
