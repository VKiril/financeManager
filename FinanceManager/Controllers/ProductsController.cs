using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinanceManager.Models;
using FinanceManager.Services;

namespace FinanceManager.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Quantity,ProductType,Cost,CostPerUnit,ForWho,IsMinimalNecesarry,PurchaseIdReceiver")] Product product)
        {
            if (ModelState.IsValid)
            {
                foreach (string upload in Request.Files)
                {
                    if (Request.Files[upload].ContentLength == 0) continue;
                    string pathToSave = Server.MapPath(GlobalConstants.PATH_UPLOADED_IMAGES);
                    if (!Directory.Exists(pathToSave))
                    {
                        Directory.CreateDirectory(pathToSave);
                    }
                    string filename = Path.GetFileName(Request.Files[upload].FileName);
                    string[] extension = filename.Split('.');

                    string serverFileName = FileUploader.GenerateUniqName() + "." + extension[extension.Length - 1];

                    Request.Files[upload].SaveAs(Path.Combine(pathToSave, serverFileName));
                    product.FileName = serverFileName;
                    product.FileOriginalName = filename;
                }

                var purchase = db.Purchases.Find(product.PurchaseIdReceiver);
                //roduct.Purchase = purchase.CastToSelf();
                product.Purchase = purchase;

                db.Products.Add(product);
                db.SaveChanges();
                return PartialView("ApiProductIndex", product);
                //return RedirectToAction("Index");
            }

            return View("ApiProductIndex", product);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Quantity,ProductType,Cost,CostPerUnit,ForWho,IsMinimalNecesarry")] Product product)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            var purchaseId = product.Purchase.ID;
            db.Products.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Details", new { controller = "Purchases", id = purchaseId });
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
