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
    public class PurchaseRequestLineItemsController : Controller
    {
        private PRS_dbContext db = new PRS_dbContext();

        private void UpdatePurchaseRequestTotal(int prid) {
            double total = 0.0;
            var purchaseRequestLineItem = db.PurchaseRequestLineItems.Where(p => p.PurchaseRequestId == prid);
                foreach (var purchaserequestlineitem in purchaseRequestLineItems) {
                var subtotal = purchaseRequestLineItem.quantity * purchaserequestlineitem.Product.Price;
                total += subtotal;
            }
            var purchaseRequest = db.PurchaseRequests.Find(prid);
            purchaseRequest.Total = total;
            db.SaveChanges();
               }

        public ActionResult List()
        {
            return Json(db.PurchaseRequestLineItems.ToList(), JsonRequestBehavior.AllowGet);


        }
        // GET: Purchase requests
        public ActionResult Get(PurchaseRequestLineItem Id)
        {
            if (Id == null)
            {
                return Json(new msg { Result = "Failure", Message = "Id is Null" }, JsonRequestBehavior.AllowGet);

            }
            PurchaseRequestLineItem purchaserequestlineitem = db.PurchaseRequestLineItems.Find(Id);                //////////
            PurchaseRequest purchaserequest = db.PurchaseRequests.Find(purchaserequestlineitem.PurchaseRequestId);     //////////
            Product product = db.Products.Find(purchaserequestlineitem.ProductId);
            if (purchaserequestlineitem == null || purchaserequest == null || product==null)                 //////////
            {
                return Json(new msg { Result = "Failure", Message = "Id not found" }, JsonRequestBehavior.AllowGet);
            }
            // if here, everything is good; we have a line item request
            return Json(purchaserequestlineitem, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add([FromBody] PurchaseRequestLineItem purchaserequestlineitem, PurchaseRequest purchaserequest, Product product)

        {
            PurchaseRequestLineItem tempPurchaseRequestLineItem = db.PurchaseRequestLineItems.Add(purchaserequestlineitem.PurchaseRequestId);  /////
            if (purchaserequestlineitem == null || purchaserequestlineitem.PurchaseRequestId == null)
            {
                return Json(new msg { Result = "Failure", Message = "Line item request parameter is missing or invalid." });
            }
            // If we get here, add the line item 
            db.PurchaseRequestLineItems.Add(purchaserequestlineitem);
            db.SaveChanges();
            UpdatePurchaseRequestTotal(purchaserequestlineitem.PurchaseRequestId);
            return Json(new msg { Result = "Success", Message = "Add Successful" });
        }
        public ActionResult Change([FromBody] PurchaseRequestLineItem purchaserequestlineitem)
        {
            if (purchaserequestlineitem == null || purchaserequestlineitem.PurchaseRequestId == null || purchaserequestlineitem.Product == null)
            {
                return Json(new msg { Result = "Failure", Message = "Purchase request parameter is missing or invalid." });
            }
            // If we get here, just update the product
            PurchaseRequestLineItem tempPurchaseRequestLineItem = db.PurchaseRequestLineItems.Find(purchaserequestlineitem.Id);
            tempPurchaseRequestLineItem.Id = purchaserequestlineitem.Id;
            tempPurchaseRequestLineItem.PurchaseRequestId = purchaserequestlineitem.PurchaseRequestId;
            tempPurchaseRequestLineItem.purchaserequest = purchaserequestlineitem.purchaserequest;
            tempPurchaseRequestLineItem.ProductId = purchaserequestlineitem.ProductId;
            tempPurchaseRequestLineItem.Product = purchaserequestlineitem.Product;
            tempPurchaseRequestLineItem.Quantity = purchaserequestlineitem.Quantity;

            db.SaveChanges();
            UpdatePurchaseRequestTotal(purchaserequestlineitem.PurchaseRequestId);
            return Json(new msg { Result = "Success", Message = "Change Successful" });

        }
        public ActionResult Remove([FromBody] PurchaseRequestLineItem purchaserequestlineitem)
        {
            if (purchaserequestlineitem == null || purchaserequestlineitem.Id <= 0)
            {
                return Json(new msg { Result = "Failure", Message = "Line item parameter is missing or invalid." });
            }
            // If we get here, delete the line item, but first we must find the id
            PurchaseRequestLineItem tempPurchaseRequestLineItem = db.PurchaseRequestLineItems.Find(purchaserequestlineitem.Id);
            if (tempPurchaseRequestLineItem == null)
            {
                return Json(new msg { Result = "Failure", Message = "Line item Id not found." });

            }
            db.PurchaseRequestLineItems.Remove(tempPurchaseRequestLineItem);
            db.SaveChanges();
            UpdatePurchaseRequestTotal(tempPurchaseRequestLineItem.PurchaseRequestId);
            return Json(new msg { Result = "Success", Message = "Remove Successful" });

        }


        // GET: PurchaseRequestLineItems
        public ActionResult Index()
        {
            var purchaseRequestLineItems = db.PurchaseRequestLineItems.Include(p => p.Product).Include(p => p.purchaserequest);
            return View(purchaseRequestLineItems.ToList());
        }

        // GET: PurchaseRequestLineItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseRequestLineItem purchaseRequestLineItem = db.PurchaseRequestLineItems.Find(id);
            if (purchaseRequestLineItem == null)
            {
                return HttpNotFound();
            }
            return View(purchaseRequestLineItem);
        }

        // GET: PurchaseRequestLineItems/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ID", "PartNumber");
            ViewBag.PurchaseRequestId = new SelectList(db.PurchaseRequests, "Id", "Description");
            return View();
        }

        // POST: PurchaseRequestLineItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PurchaseRequestId,ProductId,Quantity")] PurchaseRequestLineItem purchaseRequestLineItem)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseRequestLineItems.Add(purchaseRequestLineItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "ID", "PartNumber", purchaseRequestLineItem.ProductId);
            ViewBag.PurchaseRequestId = new SelectList(db.PurchaseRequests, "Id", "Description", purchaseRequestLineItem.PurchaseRequestId);
            return View(purchaseRequestLineItem);
        }

        // GET: PurchaseRequestLineItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseRequestLineItem purchaseRequestLineItem = db.PurchaseRequestLineItems.Find(id);
            if (purchaseRequestLineItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ID", "PartNumber", purchaseRequestLineItem.ProductId);
            ViewBag.PurchaseRequestId = new SelectList(db.PurchaseRequests, "Id", "Description", purchaseRequestLineItem.PurchaseRequestId);
            return View(purchaseRequestLineItem);
        }

        // POST: PurchaseRequestLineItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PurchaseRequestId,ProductId,Quantity")] PurchaseRequestLineItem purchaseRequestLineItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseRequestLineItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "ID", "PartNumber", purchaseRequestLineItem.ProductId);
            ViewBag.PurchaseRequestId = new SelectList(db.PurchaseRequests, "Id", "Description", purchaseRequestLineItem.PurchaseRequestId);
            return View(purchaseRequestLineItem);
        }

        // GET: PurchaseRequestLineItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseRequestLineItem purchaseRequestLineItem = db.PurchaseRequestLineItems.Find(id);
            if (purchaseRequestLineItem == null)
            {
                return HttpNotFound();
            }
            return View(purchaseRequestLineItem);
        }

        // POST: PurchaseRequestLineItems/Delete/5
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseRequestLineItem purchaseRequestLineItem = db.PurchaseRequestLineItems.Find(id);
            db.PurchaseRequestLineItems.Remove(purchaseRequestLineItem);
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
