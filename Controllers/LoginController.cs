
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stripe;
using Stripe.Checkout;
using Microsoft.AspNetCore.Http;
using CGVakBooks.DataAccess.Data;
using CGVakBooks.DataAccess.Repository;
using CGVakBooks.DataAccess.Repository.IRepository;
using CGVakBooks.Migrations;
using CGVakBooks.Models;
//using Stripe.BillingPortal;

namespace CGVakBooks.Controllers
{
    public class Login : Controller
    {
        public readonly ApplicationDbContext _db;
        string UserType = "";

        public Login(ApplicationDbContext db)
        {

            ViewBag.UserType = "customer";
            _db = db;
        }
        public IActionResult LoginUser()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            TempData["userType"] = "";
            ViewBag.UserType = "";
            HttpContext.Session.SetString("UserType", "");
            HttpContext.Session.SetString("LogStatus", "LoggedOut");
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult LoginUser(userLogin user)
        {
            if (ModelState.IsValid)
            {
                var user1 = (from e in _db.User
                             where e.Email == user.Email & e.Password == user.Password
                             select e).FirstOrDefault();
                //if (user.UserName.Equals(_db.User.UserName))
                // Product productModel = new Product();
                if (user1 == null)
                {

                    TempData["error"] = "user not exist..!";
                    Console.WriteLine("logged failed");
                    return RedirectToAction("LoginUser");
                    //return RedirectToAction("LoginUser");
                }
                else
                {
                    if (user1.UserTypeId.ToString().ToLower() == "1")
                    {
                        HttpContext.Session.SetString("UserType", "admin");
                        // HttpContext.Session.SetInt32("Age", 30);
                        // Get value from Session.
                        ViewBag.Message = HttpContext.Session.GetString("UserType");
                        // int? age = HttpContext.Session.GetInt32("Age");
                        //ViewBag.Message = "Usertype : " + user;
                        //ViewBag.UserType = "admin";
                        // HttpContext.Session.SetString(UserType,"admin");
                        //TempData["userType"] = "admin";

                        //TempData["userType"] = "admin";
                    }
                    else
                    {
                        //ViewBag.UserType = "admin";
                        HttpContext.Session.SetString("UserType", "customer");
                        ViewBag.Message = HttpContext.Session.GetString("UserType");
                        //TempData["userType"] = HttpContext.Session.GetString(UserType);
                        //TempData["userType"] = "customer";
                    }
                    HttpContext.Session.SetInt32("UserId", user1.Id);
                    HttpContext.Session.SetString("LogStatus", "LoggedIn");
                    TempData["success"] = "logged sucessfully...";
                    Console.WriteLine("logged succ");
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();

        }
    }
}