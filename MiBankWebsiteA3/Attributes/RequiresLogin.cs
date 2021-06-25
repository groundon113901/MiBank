using MiBankWebsiteA2.Data;
using MiBankWebsiteA2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace MiBankWebsiteA2.Attributes
{
    //This code was taken from Matthew Bolger
    //OUA RMIT Lecturer.
    //Found in the Week 7 Tutorial example.
    public class RequiresLogin :Attribute, IAuthorizationFilter
    {
        private readonly MiBankContext _context;
        public RequiresLogin(MiBankContext context)
        {
            _context = context;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var CustomerID = context.HttpContext.Session.GetInt32(nameof(Customer.CustomerID));
            //If not logged in, redirect to Login Page

            if (!CustomerID.HasValue)
            {
                context.Result = new RedirectToActionResult("Login", "Login", null);
            }

            var CustomerAccount = _context.Logins.Where(x => x.CustomerID == CustomerID).FirstOrDefault();
            //If Account is Locked, Clear session and redirect to login page.
            if (CustomerAccount != null && CustomerAccount.Locked)
            {
                context.HttpContext.Session.Clear();
                context.Result = new RedirectToActionResult("Login", "Login", null);
            }
        }
    }
}
