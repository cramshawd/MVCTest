using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Evaluation_1.Models;
using MVC_Evaluation_1.ViewModels;

namespace MVC_Evaluation_1.Controllers
{
    public class UsersController : Controller
    {
        private TestContext db;
        private UserService svcUser = new UserService();

        public UsersController()
        {
            db = new TestContext();
        }
        
        public UsersController(TestContext testdb)
        {
            db = testdb;
        }
        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    UserViewModel vm = new UserViewModel(user);
        //    return View(vm);
        //}

        // GET: Users/Edit
        public ActionResult Edit(int id)
        {
            User user = db.Users.Find(id);

            var vm = new UserViewModel(user);
            return View(vm);
        }

        // POST: Users/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            //Removed for Unit Testing
            //using (var db = new TestContext())
            //{       }
            var user = db.Users.Find(id);
            user.updateUser(vm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            UserViewModel vm = new UserViewModel();
            vm.IsActive = true;
            return View(vm);
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            if (svcUser.emailExists(vm.Email))
            {
                ModelState["Email"].Errors.Add("A user with this email address already exists");
                return View(vm);
            }
            var u = new User(
                email: vm.Email,
                lastName: vm.LastName,
                firstName: vm.FirstName,
                phoneNumber: vm.PhoneNumber,
                empType: db.EmployeeTypes.Find(int.Parse(vm.EmpTypeSelected)),
                companyName: vm.CompanyName,
                isActive: vm.IsActive,
                createdBy: -1,
                createdDate: DateTime.UtcNow
            );
            db.Users.Add(u);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
