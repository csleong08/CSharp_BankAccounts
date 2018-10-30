using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankAccounts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;


namespace BankAccounts.Controllers
{
    public class HomeController : Controller
    {
        private BankAccountsContext _context;
    
        public HomeController(BankAccountsContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index(Users myUser)
        {
            return View("Index");
        }
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost("create")]
        public IActionResult AddUser(UsersValidate uservalidator)
        {
            if(ModelState.IsValid)
            {
                PasswordHasher<UsersValidate> Hasher = new PasswordHasher<UsersValidate>();
                uservalidator.password = Hasher.HashPassword(uservalidator, uservalidator.password);
                Users myUser = new Users();
                myUser.firstname = uservalidator.firstname;
                myUser.lastname = uservalidator.lastname;
                myUser.email = uservalidator.email;
                myUser.password = uservalidator.password;
                myUser.created_at = DateTime.Now;
                myUser.updated_at = DateTime.Now;
                _context.Add(myUser);
                _context.SaveChanges();

                HttpContext.Session.SetInt32("UserID", (int)myUser.usersid);
                int? UserID = HttpContext.Session.GetInt32("UserID");
                ViewBag.UserID = UserID;
                return RedirectToAction("Account");
            }
            else
            {
                return View("Index");
            }
        }
        [HttpGet("Account")]
        public IActionResult Account()
        {
            int? UserID = HttpContext.Session.GetInt32("UserID");
            var userInfo = _context.users.Where(p => p.usersid == (long)UserID).SingleOrDefault();
            ViewBag.userInfo = userInfo;
            // List<Users> allData = _context.users.Where(p => p.usersid == UserID).Include(p => p.Transaction).ToList();
            List<Transactions> allData = _context.transactions.Where(p => p.usersid == UserID).OrderByDescending(p => p.created_at).ToList();
            ViewBag.allData = allData;
            double sum = _context.transactions.Where(p => p.usersid == (long)UserID).Sum(p => p.amount);
            ViewBag.sum = sum;
            return View("Account");
        }
        [HttpPost("LoginProcess")]
        public IActionResult LoginProcess(Logins myLogin)
        {
        if(ModelState.IsValid)
            {
                Users userData = _context.users.SingleOrDefault(p => p.email == myLogin.email);
                if(userData != null && myLogin.password != null)
                {
                    var Hasher = new PasswordHasher<Users>();
                    // Pass the user object, the hashed password, and the PasswordToCheck
                    if(0 != Hasher.VerifyHashedPassword(userData, userData.password, myLogin.password))
                    {
                        HttpContext.Session.SetInt32("UserID", (int)userData.usersid);
                        int? UserID = HttpContext.Session.GetInt32("UserID");
                        ViewBag.UserID = UserID;
                        return RedirectToAction("Account");
                    }
                }
                return View("Login");
            }
            return View("Login");
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
        [HttpPost("AddTransaction")]
        public IActionResult AddTransaction(double amount)
        {
            int? UserID = HttpContext.Session.GetInt32("UserID");
            var userInfo = _context.users.Where(p => p.usersid == (long)UserID).SingleOrDefault();
            Transactions myTransaction = new Transactions();
            myTransaction.amount = amount;
            myTransaction.usersid = (long)UserID;
            myTransaction.created_at = DateTime.Now;
            myTransaction.updated_at = DateTime.Now;
            _context.Add(myTransaction);
            _context.SaveChanges();
            return RedirectToAction("Account");
        }
    }
}
