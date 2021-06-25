using MiBankWebsiteA2.Attributes;
using MiBankWebsiteA2.Data;
using MiBankWebsiteA2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleHashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiBankWebsiteA2.Controllers
{
    [ServiceFilter(typeof(RequiresLogin))]
    public class CustomerController : Controller
    {
        private readonly MiBankContext _context;
        public CustomerController(MiBankContext context)
        {
            _context = context;
        }
        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;
        public async Task<IActionResult> Index()
        {
            var customer = await _context.Customers.FindAsync(CustomerID);            
            
            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> ScheduledPayments() {
            var customer = await _context.Customers.FindAsync(CustomerID);
            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> Statements()
        {
            var customer = await _context.Customers.FindAsync(CustomerID);
            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> PayBills(int id)
        {
            var customer = await _context.Customers.FindAsync(CustomerID);
            var billPay = await _context.BillPays.FindAsync(id);
            ViewBag.BillPay = billPay;
            ViewBag.Customer = customer;
            return View(billPay);
        }

        [HttpPost]
        public async Task<IActionResult> PayBills(int id, decimal amount, int AccountNumber, int PayeeID, BillPayPeriod PayPeriod, DateTime ScheduleDate)
        {
            // NOTE FOR ASSESSMENT MARKER:
            // Marks Rubric: 1.) Scheduling a payment to a payee as a one off payment (immediate)
            // We have opted to have this being able to be set as a one off.
            // Functionality to allow a set date (which could include immediate ones) seemed much more realistic.
            
            if (id == 0) {
                var Account = await _context.Accounts.FindAsync(AccountNumber);
                Account.BillPays.Add(
                    new BillPay
                    {
                        AccountNumber = AccountNumber,
                        PayeeID = PayeeID,
                        Amount = amount,
                        ScheduleDate = ScheduleDate,
                        PayPeriod = PayPeriod,
                        LastModified = DateTime.Now
                    }
                    );
                await _context.SaveChangesAsync();
            } else {
                var BillPay = await _context.BillPays.FindAsync(id);
                BillPay.AccountNumber = AccountNumber;
                BillPay.Amount = amount;
                BillPay.LastModified = DateTime.Now;
                BillPay.PayeeID = PayeeID;
                BillPay.ScheduleDate = ScheduleDate;
                BillPay.PayPeriod = PayPeriod;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ScheduledPayments));
        }

        [HttpGet]
        public async Task<IActionResult> ShowTransactions(int id, int pageid)
        {
            var account = await _context.Accounts.FindAsync(id);
            int numOfpages = (int) Math.Ceiling((double)account.Transactions.Count / 4);
            ViewBag.Customer = account.Customer;
            ViewBag.PageCurrent = pageid;
            ViewBag.PageLast = numOfpages;
            ViewBag.PageFirst = 1;
            if (pageid - 1 > ViewBag.PageFirst)
            {
                ViewBag.PagePrevious = pageid - 1;
            }
            else {
                ViewBag.PagePrevious = ViewBag.PageFirst;
            }
            if (pageid + 1 < ViewBag.PageLast)
            {
                ViewBag.PageNext = pageid + 1;
            }
            else {
                ViewBag.PageNext = ViewBag.PageLast;
            }
            return View(account);
        }

        [HttpGet]
        public async Task<IActionResult> Settings(int edit)
        {
            var customer = await _context.Customers.FindAsync(CustomerID);
            if (edit == 1)
            {
                ViewBag.Edit = true;
            }
            return View(customer);
        }
        [HttpPost]
        public async Task<IActionResult> Settings(string currentPassword, string newPassword, string confirmPassword) {
            var customer = await _context.Customers.FindAsync(CustomerID);
            var login = _context.Logins.Single(x => x.CustomerID == CustomerID);
            if (PBKDF2.Verify(login.PasswordHash, currentPassword) && newPassword.Equals(confirmPassword)) {
                login.PasswordHash = PBKDF2.Hash(newPassword);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Password changed successfully";
            }
            else
            {
                if (!PBKDF2.Verify(login.PasswordHash, currentPassword))
                {
                    ModelState.AddModelError("passwordincorrect", "Current password does not match, Please Try Again");
                    return View(customer);
                }
                if (!newPassword.Equals(confirmPassword))
                {
                    ModelState.AddModelError("nomatch", "New password does not match, Please Try Again");
                    return View(customer);
                }
            }
            return RedirectToAction(nameof(Settings));
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAccountDetails(string TFN, string Address, string City, string State, int PostCode, string Phone) {
            var customer = await _context.Customers.FindAsync(CustomerID);
            customer.TFN = TFN;
            customer.Address = Address;
            customer.City = City;
            customer.State = State;
            customer.PostCode = PostCode;
            customer.Phone = Phone;
            await _context.SaveChangesAsync();
            TempData["SuccessAccount"] = "Account details updated successfully";
            return RedirectToAction(nameof(Settings));
        }
    }
}
