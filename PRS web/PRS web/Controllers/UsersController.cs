using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PRS_web.Models;
using Utility;
using System.Web.Http;

namespace PRS_web.Controllers
{
    public class UsersController : Controller
    {
        private PRS_dbContext db = new PRS_dbContext();

        public ActionResult List() {
            //This will ALWAYS return an array
            // it may have 0, 1 or more items within the array
            return Json(db.Users.ToList(), JsonRequestBehavior.AllowGet);

        }
            //this will return one user or an error emssage
        public ActionResult Get(int? id) {
            //error if nothing is passed in for ID
            if (id == null) {
                return Json(new msg { Result = "Failure", Message = "Id is Null" }, JsonRequestBehavior.AllowGet);
            
                
            }
            //return a user for the id or null if not found
            User user = db.Users.Find(id);
            //this is true if the id is not found
            if(user == null){
                return Json(new msg { Result = "Failure", Message = "Id not found"}, JsonRequestBehavior.AllowGet);
            }
            // if here, everything is good; we have a user
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add([FromBody] User user) {
            if(user== null || user.UserName== null) {
                return Json(new msg{Result ="Failure", Message="User parameter is missing or invalid." });
            }
            // If we get here, add the user
            db.Users.Add(user);
            db.SaveChanges();
            return Json(new msg { Result = "Success", Message = "Add Successful" });
        }
        public ActionResult Change([FromBody] User user) {
            if (user == null || user.UserName == null)
            {
                return Json(new msg { Result = "Failure", Message = "User parameter is missing or invalid." });
            }
            // If we get here, just update the user
            User tempUser = db.Users.Find(user.id);
            tempUser.UserName = user.UserName;
            tempUser.Password = user.Password;
            tempUser.FirstName = user.FirstName;
            tempUser.LastName = user.LastName;
            tempUser.Phone = user.Phone;
            tempUser.Email = user.Email;
            tempUser.IsReviewer = user.IsReviewer;
            tempUser.IsAdmin = user.IsAdmin;
            db.SaveChanges();
            return Json(new msg { Result = "Success", Message = "Change Successful" });

        }
            
        public ActionResult Remove([FromBody] User user){
            if (user == null || user.id <= 0){
                return Json(new msg { Result = "Failure", Message = "User parameter is missing or invalid." });
            }
            // If we get here, delete the user, but first we must find the user
            User tempUser = db.Users.Find(user.id);
            if(tempUser == null) {
                return Json(new msg { Result = "Failure", Message = "User Id not found." });

            }
            db.Users.Remove(tempUser);
            db.SaveChanges();
            return Json(new msg { Result = "Success", Message = "Remove Successful" });

        }

        #region MVC Methods

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,UserName,Password,FirstName,LastName,Phone,Email,IsReviewer,IsAdmin")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,UserName,Password,FirstName,LastName,Phone,Email,IsReviewer,IsAdmin")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

#endregion

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
