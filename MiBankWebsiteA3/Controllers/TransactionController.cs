using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiBankWebsiteA2.Attributes;
using MiBankWebsiteA2.Data;
using MiBankWebsiteA2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiBankWebsiteA2.Controllers
{
    [ServiceFilter(typeof(RequiresLogin))]
    public class TransactionController : Controller
    {
        private readonly MiBankContext _context;
        public TransactionController(MiBankContext context)
        {
            _context = context;
        }
        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;
        public IActionResult Index()
        {
            return RedirectToAction("ATM");
        }

        public async Task<IActionResult> ATM()
        {
            var customer = await _context.Customers.FindAsync(CustomerID);
            ViewBag.Customer = customer;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ATM(int AccountNumber, decimal Amount, string Comment, int DestAccountNumber, string transactionType) {
            var account = await _context.Accounts.FindAsync(AccountNumber);

            if (account.FeeRequired(account))
            {
                if (transactionType == "Withdraw")
                    Amount += account.ATMfee;
                if (transactionType == "Transfer")
                    Amount += account.Transferfee;
            }

            switch (transactionType) {
                case "Deposit":
                    account.deposit(Amount, Comment);
                    TempData["Success"] = "Deposit was successful";
                    break;

                case "Transfer":
                    if (account.Balance < Amount)
                        ModelState.AddModelError("Amount", "Account does not have enough money to process this transaction.");
                    if (DestAccountNumber == AccountNumber) {
                        ModelState.AddModelError("DestAccountNumber", "DestAccountNumber and Account can not be the same");
                    }
                    if (!ModelState.IsValid)
                    {
                        ViewBag.Amount = Amount;
                        var customer = await _context.Customers.FindAsync(CustomerID);
                        ViewBag.Customer = customer;
                        return View();
                    }
                    var DestAccount = await _context.Accounts.FindAsync(DestAccountNumber);
                    account.transfer(Amount, $"Transfer from Account {AccountNumber} to Account {DestAccountNumber}", DestAccount);
                    TempData["Success"] = "Transfer was successful";
                    break;

                case "Withdraw":
                    if (account.Balance < Amount)
                        ModelState.AddModelError("Amount", "Account does not have enough money to process this transaction.");
                    if (!ModelState.IsValid)
                    {
                        var customer = await _context.Customers.FindAsync(CustomerID);
                        ViewBag.Customer = customer;
                        return View();
                    }
                    account.withdraw(Amount, Comment);
                    TempData["Success"] = "Withdraw was successful";
                    break;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
