using LittleStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;

namespace LittleStore.Controllers
{

    //[Authorize(Roles = "Администратор, Модератор, Исполнитель")]
    public class UserController : Controller
    {
        private LittleStoreContext db = new LittleStoreContext();

        [HttpGet]
        public ActionResult Index()
        {
            //var users = db.Users.Include(u => u.Role).ToList();
            var users = (from u in db.Users select u).ToList(); 
            return View(users);
        }

        public ActionResult Edit(int id)
        {
            User user = db.Users.Find(id);

            SelectList roles = new SelectList(db.Roles, "RoleId", "Name", user.RoleId);
            ViewBag.Roles = roles;

            return View(user);

        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            SelectList roles = new SelectList(db.Roles, "RoleId", "Name");
            ViewBag.Roles = roles;

            return View(user);
        }

        public ActionResult Delete(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
