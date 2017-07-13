using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace MVC_Evaluation_1.Models
{
    public class UserService
    {

        public User getUser(int Id)
        {
            TestContext db = new TestContext();
            return db.Users.Find(Id);
        }
        public User getUser(string Email) {
            var user = new User();
            TestContext db = new TestContext();
            user = db.Users.Where(u => u.Email.ToUpper() == Email.ToUpper()).FirstOrDefault();
            return user;
        }

        public int getUserIdByEmail(string Email)
        {
            User user = new User();
            TestContext db = new TestContext();
            user = db.Users.Where(u => u.Email.ToUpper() == Email.ToUpper()).FirstOrDefault();
            return user.Id;
        }

        public bool emailExists(string Email)
        {
            User user = new User();
            TestContext db = new TestContext();
            user = db.Users.Where(u => u.Email.ToUpper() == Email.ToUpper()).FirstOrDefault();
            if (user == null) return false;
            return true;
        }

        public bool canUserLogin(User u)
        {
            bool canLogin = false;
            if (u.IsActive)
            {
                canLogin = true;
            } else
            {
                canLogin = false;
            }
            return canLogin;
        }


        public IEnumerable<SelectListItem> getEmployeeTypeList()
        {
            TestContext db = new TestContext();
            return db.EmployeeTypes.OrderBy(e => e.SortOrder).Select(e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() }).ToList();
        }



    }
}