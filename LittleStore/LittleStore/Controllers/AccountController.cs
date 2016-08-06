using LittleStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;

namespace LittleStore.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registration() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(User user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                LittleStoreContext db = new LittleStoreContext();
                user.RoleId = 3;
                user.DateReg = DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();
            }
            return RedirectToAction("Login", "Account");
        }

        private bool ValidateUser(string login, string password)
        {
            bool isValid = false;
            using (LittleStoreContext db = new LittleStoreContext())
            {
                try
                {
                    User user = (from u in db.Users
                                 where u.Login == login && u.Password == password
                                 select u).FirstOrDefault();

                    if (user != null)
                    {
                        isValid = true;
                    }
                }
                catch
                {
                    isValid = false;
                }
            }
            return isValid;
            //return true;
        }

    }
}
