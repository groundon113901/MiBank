using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using MiBankWebsiteA2.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Angular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController
    {
        private readonly CustomerDataAccessLayer _customerDataAccessLayer;

        public CustomerController(CustomerDataAccessLayer customerDataAccessLayer)
        {
            _customerDataAccessLayer = customerDataAccessLayer;
        }

        [HttpGet]
        [Route("Index")]
        public IEnumerable<Customer> Get()
        {
            return _customerDataAccessLayer.GetAllCustomers();
        }

        [HttpGet]
        [Route("Details/{id}")]
        public Customer Details(int id)
        {
            return _customerDataAccessLayer.GetCustomerData(id);
        }

        [HttpPut]
        [Route("Edit")]
        public int Edit(Customer customer)
        {
            return _customerDataAccessLayer.UpdateCustomer(customer);
        }
    }
}
