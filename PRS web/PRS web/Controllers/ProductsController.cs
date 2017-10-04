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
    public class ProductsController : Controller
    {
        private PRS_dbContext db = new PRS_dbContext();

        public ActionResult List()
        {
            return Json(db.Products.ToList(), JsonRequestBehavior.AllowGet);


        }
        // GET: Vendors
        public ActionResult Get(Vendor ID)
        {
            if (ID == null)
            {
                return Json(new msg { Result = "Failure", Message = "Id is Null" }, JsonRequestBehavior.AllowGet);

            }
            Product product = db.Products.Find(ID);                
               
            
            if (product == null)                 
            {
                return Json(new msg { Result = "Failure", Message = "Id not found" }, JsonRequestBehavior.AllowGet);
            }
            return Json(product, JsonRequestBehavior.AllowGet);
           
        }
        public ActionResult Add([FromBody] Product product) {
            if (product == null || product.PartNumber ==null )
            {
                return Json(new msg { Result = "Failure", Message = "Product is null" });
            }
            //**Foreign key issue:
            Vendor vendor = db.Vendors.Find(product.VendorId); //returns a vendor for the ID or null if not found          
                      
            if (vendor == null)
            {
                return Json(new msg { Result = "Failure", Message = "Vendor Id FK is missing or invalid." });
            }
            
            // If we get here, add the Product                            
            db.Products.Add(product);
            db.SaveChanges();
            return Json(new msg { Result = "Success", Message = "Add Successful" });
        }
        public ActionResult Change([FromBody] Product product)
        {
            if (product == null || product.PartNumber == null)
            {
                return Json(new msg { Result = "Failure", Message = "Product parameter is missing or invalid." });
            }
            //Foreign key 
            Vendor vendor = db.Vendors.Find(product.VendorId); 
            if (vendor == null) 
            {
                return Json(new msg { Result = "Failure", Message = "Vendor Id not found" });
            }

            // If we get here, just update the product
            Product tempProduct = db.Products.Find(product.ID);
            tempProduct.ID = product.ID;
            tempProduct.VendorId = product.VendorId;
            tempProduct.vendor = product.vendor;
            tempProduct.PartNumber = tempProduct.PartNumber;
            tempProduct.Name = product.Name;
            tempProduct.Price = product.Price;
            tempProduct.Unit = product.Unit;
            tempProduct.PhotoPath = product.PhotoPath;
            db.SaveChanges();
            return Json(new msg { Result = "Success", Message = "Change Successful" });

        }
        public ActionResult Remove([FromBody] Product product)
        {
            if (product == null || product.ID <= 0)
            {
                return Json(new msg { Result = "Failure", Message = "Product parameter is missing or invalid." });
            }
            // If we get here, delete the vendor, but first we must find the vendor
            Product tempProduct = db.Products.Find(product.ID);
            if (tempProduct == null)
            {
                return Json(new msg { Result = "Failure", Message = "Product Id not found." });

            }
            db.Products.Remove(tempProduct);
            db.SaveChanges();
            return Json(new msg { Result = "Success", Message = "Remove Successful" });

        }
        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,VendorId,PartNumber,Name,Price,Unit,PhotoPath")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,VendorId,PartNumber,Name,Price,Unit,PhotoPath")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
