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
    public class PurchaseRequestsController : Controller
    {
        private PRS_dbContext db = new PRS_dbContext();

        public ActionResult List()
        {
            //return Json(db.PurchaseRequests.ToList(), JsonRequestBehavior.AllowGet); 
            return new JsonNetResult { Data = db.PurchaseRequests.ToList() };

        }
        // GET: Purchase requests
        public ActionResult Get(User Id)
        {
            if (Id == null)
            {
                return Json(new msg { Result = "Failure", Message = "Id is Null" }, JsonRequestBehavior.AllowGet);

            }
            PurchaseRequest purchaserequest = db.PurchaseRequests.Find(Id);                //////////
            User User = db.Users.Find(purchaserequest.UserId);     //////////

            if (purchaserequest == null || User == null)                 //////////
            {
                return Json(new msg { Result = "Failure", Message = "Id not found" }, JsonRequestBehavior.AllowGet);
            }
            // if here, everything is good; we have a Purchase request
            return Json(purchaserequest, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add([FromBody] PurchaseRequest purchaserequest, User Userid)

        {
            User tempUser = db.Users.Add(purchaserequest.UserId);  /////
            if (purchaserequest == null || purchaserequest.UserId == null)
            {
                return Json(new msg { Result = "Failure", Message = "Purchase request parameter is missing or invalid." });
            }
            // If we get here, add the Product
            db.Users.Add(purchaserequest.UserId);                             ////// 
            db.PurchaseRequests.Add(purchaserequest);
            db.SaveChanges();
            return Json(new msg { Result = "Success", Message = "Add Successful" });
        }
        public ActionResult Change([FromBody] PurchaseRequest purchaserequest)
        {
            if (purchaserequest == null || purchaserequest.UserId == null)
            {
                return Json(new msg { Result = "Failure", Message = "Purchase request parameter is missing or invalid." });
            }
            // If we get here, just update the product
            PurchaseRequest tempPurchaseRequest = db.PurchaseRequests.Find(purchaserequest.Id);
            tempPurchaseRequest.Id = purchaserequest.Id;
            tempPurchaseRequest.UserId = purchaserequest.UserId;
            tempPurchaseRequest.User = purchaserequest.User;
            tempPurchaseRequest.Description = purchaserequest.Description;
            tempPurchaseRequest.Justification = purchaserequest.Justification;
            tempPurchaseRequest.DateNeeded = purchaserequest.DateNeeded;
            tempPurchaseRequest.DeliveryMode = purchaserequest.DeliveryMode;
            tempPurchaseRequest.Status = purchaserequest.Status;
            tempPurchaseRequest.Total = purchaserequest.Total;
            tempPurchaseRequest.SubmittedDate = purchaserequest.SubmittedDate;
            db.SaveChanges();
            return Json(new msg { Result = "Success", Message = "Change Successful" });

        }
        public ActionResult Remove([FromBody] PurchaseRequest purchaserequest)
        {
            if (purchaserequest == null || purchaserequest.Id <= 0)
            {
                return Json(new msg { Result = "Failure", Message = "Purchase request parameter is missing or invalid." });
            }
            // If we get here, delete the Purchase request, but first we must find the id
            PurchaseRequest tempPurchaseRequest = db.PurchaseRequests.Find(purchaserequest.Id);
            if (tempPurchaseRequest == null)
            {
                return Json(new msg { Result = "Failure", Message = "Product Id not found." });

            }
            db.PurchaseRequests.Remove(tempPurchaseRequest);
            db.SaveChanges();
            return Json(new msg { Result = "Success", Message = "Remove Successful" });

        }
        // GET: PurchaseRequests
        public ActionResult Index()
        {
            var purchaseRequests = db.PurchaseRequests.Include(p => p.User);
            return View(purchaseRequests.ToList());
        }

        // GET: PurchaseRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseRequest purchaseRequest = db.PurchaseRequests.Find(id);
            if (purchaseRequest == null)
            {
                return HttpNotFound();
            }
            return View(purchaseRequest);
        }

        // GET: PurchaseRequests/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "id", "UserName");
            return View();
        }

        // POST: PurchaseRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Description,Justification,DateNeeded,DeliveryMode,Status,Total,SubmittedDate")] PurchaseRequest purchaseRequest)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseRequests.Add(purchaseRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "id", "UserName", purchaseRequest.UserId);
            return View(purchaseRequest);
        }

        // GET: PurchaseRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseRequest purchaseRequest = db.PurchaseRequests.Find(id);
            if (purchaseRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "id", "UserName", purchaseRequest.UserId);
            return View(purchaseRequest);
        }

        // POST: PurchaseRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Description,Justification,DateNeeded,DeliveryMode,Status,Total,SubmittedDate")] PurchaseRequest purchaseRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "id", "UserName", purchaseRequest.UserId);
            return View(purchaseRequest);
        }

        // GET: PurchaseRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseRequest purchaseRequest = db.PurchaseRequests.Find(id);
            if (purchaseRequest == null)
            {
                return HttpNotFound();
            }
            return View(purchaseRequest);
        }

        // POST: PurchaseRequests/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseRequest purchaseRequest = db.PurchaseRequests.Find(id);
            db.PurchaseRequests.Remove(purchaseRequest);
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
