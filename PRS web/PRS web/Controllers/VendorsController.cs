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
    public class VendorsController : Controller
    {
        private PRS_dbContext db = new PRS_dbContext();

        public ActionResult List() {
            return Json(db.Vendors.ToList(), JsonRequestBehavior.AllowGet);


        }
        // GET: Vendors
        public ActionResult Get(int? ID)
        {
            if (ID == null)
            {
                return Json(new msg { Result = "Failure", Message = "Id is Null" }, JsonRequestBehavior.AllowGet);

            }


            Vendor vendor = db.Vendors.Find(ID);
            if (vendor == null)
            {
                return Json(new msg { Result = "Failure", Message = "Id not found" }, JsonRequestBehavior.AllowGet);
            }
            // if here, everything is good; we have a user
            return Json(vendor, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add([FromBody] Vendor vendor)
        {
            if (vendor == null || vendor.Name == null)
            {
                return Json(new msg { Result = "Failure", Message = "Vendor parameter is missing or invalid." });
            }
            // If we get here, add the user
            db.Vendors.Add(vendor);
            db.SaveChanges();
            return Json(new msg { Result = "Success", Message = "Add Successful" });
        }
        public ActionResult Change([FromBody] Vendor vendor)
        {
            if (vendor == null || vendor.Name == null)
            {
                return Json(new msg { Result = "Failure", Message = "Vendor parameter is missing or invalid." });
            }
            // If we get here, just update the vendor
            Vendor tempVendor = db.Vendors.Find(vendor.ID);
            tempVendor.Code = vendor.Code;
            tempVendor.Name = vendor.Name;
            tempVendor.Address = vendor.Address;
            tempVendor.City = vendor.City;
            tempVendor.State = vendor.State;
            tempVendor.Zip = vendor.Zip;
            tempVendor.Phone = vendor.Phone;
            tempVendor.Email = vendor.Email;
            tempVendor.IsPreApproved = vendor.IsPreApproved;
            db.SaveChanges();
            return Json(new msg { Result = "Success", Message = "Change Successful" });

        }
        public ActionResult Remove([FromBody] Vendor vendor)
        {
            if (vendor == null || vendor.ID <= 0)
            {
                return Json(new msg { Result = "Failure", Message = "Vendor parameter is missing or invalid." });
            }
            // If we get here, delete the vendor, but first we must find the vendor
            Vendor tempVendor = db.Vendors.Find(vendor.ID);
            if (tempVendor == null)
            {
                return Json(new msg { Result = "Failure", Message = "Vendor Id not found." });

            }
            db.Vendors.Remove(tempVendor);
            db.SaveChanges();
            return Json(new msg { Result = "Success", Message = "Remove Successful" });

        }
        #region MVC Methods
        // GET: Vendors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        // GET: Vendors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vendors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Code,Name,Address,City,State,Zip,Phone,Email,IsPreApproved")] Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                db.Vendors.Add(vendor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vendor);
        }

        // GET: Vendors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        // POST: Vendors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Code,Name,Address,City,State,Zip,Phone,Email,IsPreApproved")] Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vendor);
        }

        // GET: Vendors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        // POST: Vendors/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vendor vendor = db.Vendors.Find(id);
            db.Vendors.Remove(vendor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
# endregion
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
