using Microsoft.AspNetCore.Mvc;
using MiBankWebsiteA2.Data;
using System.Threading.Tasks;
using SimpleHashing;
using MiBankWebsiteA2.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace MiBankWebsiteA2.Controllers
{
    public class LoginController : Controller
    {
        private readonly MiBankContext _context;
        public LoginController(MiBankContext context)
        {
            _context = context;
        }

        [HttpGet] //Default, Here so i remember what does what.
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(int LoginID, string PasswordHash)
        {
            var login = await _context.Logins.FindAsync(LoginID);
            var loginKey = nameof(Customer.CustomerID) + "LoginCounter";

            //Resetting LoginKey to appropriate value.
            var loginCounter = 0;
            if (HttpContext.Session.GetInt32(loginKey) == null)
                HttpContext.Session.SetInt32(loginKey, 0);
            else
                loginCounter = (int)HttpContext.Session.GetInt32(loginKey);

            //Unsuccessful Login
            //Running fail logic
            if (login == null)
            {
                ModelState.AddModelError("LoginFailed", "Login Failed, No Account found.");
                return View(new Login { LoginID = LoginID });
            }
            //Account Locked
            if (login.Locked)
            {
                if ((DateTime.Now - login.ModifyDate).TotalMinutes < 1)
                {
                    ModelState.AddModelError("LoginFailed", "This account is locked. Please try again later.");
                    return View(new Login { LoginID = LoginID });
                }

                login.Locked = false;
                await _context.SaveChangesAsync();
            }
            if (!PBKDF2.Verify(login.PasswordHash, PasswordHash))
            {
                loginCounter++;
                HttpContext.Session.SetInt32(loginKey, loginCounter);
                if (loginCounter >= 3 && !login.Locked)
                {
                    //Update Database to lock account
                    login.Locked = true;
                    login.ModifyDate = DateTime.Now;
                    await _context.SaveChangesAsync();
                    HttpContext.Session.SetInt32(loginKey, 0);
                    ModelState.AddModelError("LoginFailed", "Login Failed - This account has been locked.");
                }
                else
                {
                    ModelState.AddModelError("LoginFailed", "Login Failed, Please Try Again");
                }
                return View(new Login { LoginID = LoginID });
            }
            //Successful Login
            HttpContext.Session.SetInt32(nameof(Customer.CustomerID), login.CustomerID);
            HttpContext.Session.SetString(nameof(Customer.Name), login.Customer.Name);

            var customer = await _context.Customers.FindAsync(HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value);
            customer.updateBillPays();
            await _context.SaveChangesAsync();

            return RedirectToAction("ATM", "Transaction");
        }
        public IActionResult Admin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Admin(string LoginID, string PasswordHash)
        {
            if (LoginID == "Admin" && PasswordHash == "Admin")
            {
                //Update this to redirect to admin portal
                return Redirect("http://localhost:5000");
            }
            if (LoginID != "Admin")
            {
                ModelState.AddModelError("LoginFailed", "Login Failed - Account could not be found");
            }
            if (PasswordHash != "Admin")
            {
                ModelState.AddModelError("LoginFailed", "Login Failed - Password was incorrect!");
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
