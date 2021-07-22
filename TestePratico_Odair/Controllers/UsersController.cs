using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestePratico_Odair.Models;
using static TestePratico_Odair.Class.UserHelper;
using PagedList;

namespace TestePratico_Odair.Controllers
{
    public class UsersController : Controller
    {
        private TestePratico_OdairContext db = new TestePratico_OdairContext();

        [Authorize(Roles = "Admin, User")]
        // GET: Users
        //public ActionResult Index()
        //{
        //    return View(db.Users.ToList());
        //}


        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NomeParam = String.IsNullOrEmpty(sortOrder) ? "Nome_desc" : "";
            ViewBag.LastNameParam = sortOrder == "SobreNome" ? "SobreNome_desc" : "SobreNome";
            ViewBag.CellPhoneParam = sortOrder == "CellPhone" ? "CellPhone_desc" : "CellPhone";
            ViewBag.RGParam = sortOrder == "RG" ? "RG_desc" : "RG";
            ViewBag.CPFParam = sortOrder == "CPF" ? "CPF_desc" : "CPF";
            ViewBag.EmailParam = sortOrder == "UserName" ? "UserName_desc" : "UserName";
            ViewBag.UserIdParam = sortOrder == "UserId" ? "UserId_desc" : "UserId";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var users = from s in db.Users
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.UserName.ToUpper().Contains(searchString.ToUpper())
                                       || s.FirstName.ToUpper().Contains(searchString.ToUpper())
                                       || s.LastName.ToUpper().Contains(searchString.ToUpper())
                                       || s.UserName.ToUpper().Contains(searchString.ToUpper())
                                       || s.RG.ToUpper().Contains(searchString.ToUpper())
                                       || s.CPF.ToUpper().Contains(searchString.ToUpper())
                                       || s.CellPhone.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Nome_desc":
                    users = users.OrderBy(s => s.FirstName);
                    break;
                case "SobreNome":
                    users = users.OrderBy(s => s.LastName);
                    break;
                case "SobreNome_desc":
                    users = users.OrderBy(s => s.LastName);
                    break;
                case "RG":
                    users = users.OrderBy(s => s.RG);
                    break;
                case "RG_desc":
                    users = users.OrderBy(s => s.RG);
                    break;
                case "CPF":
                    users = users.OrderBy(s => s.CPF);
                    break;
                case "CPF_desc":
                    users = users.OrderBy(s => s.CPF);
                    break;
                case "CellPhone":
                    users = users.OrderBy(s => s.CellPhone);
                    break;
                case "CellPhone_desc":
                    users = users.OrderBy(s => s.CellPhone);
                    break;
                case "UserName":
                    users = users.OrderBy(s => s.UserName);
                    break;
                case "UserName_Desc":
                    users = users.OrderBy(s => s.UserName);
                    break;
                case "UserID_desc":
                    users = users.OrderBy(s => s.UserId);
                    break;
                default:
                    users = users.OrderBy(s => s.FirstName);
                    break;
            }


            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        }




        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();

                UsersHelper.CreateUserASP(users.UserName, "User");
                return RedirectToAction("Index");
            }

            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Users users)
        {
            if (ModelState.IsValid)
            {

                var db2 = new TestePratico_OdairContext();
                var currentUser = db2.Users.Find(users.UserId);
                if (currentUser.UserName != users.UserName)
                {

                    UsersHelper.UpdateUser(currentUser.UserName, users.UserName);


                }
                db2.Dispose();


                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
            db.SaveChanges();
            UsersHelper.DeleteUser(users.UserName);



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
