﻿using System;
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
    public class PurchasesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Purchases
        //[ActionName("/purchases")]
        public ActionResult Index()
        {
            return View(db.Purchases.ToList());
        }

        // GET: Purchases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // GET: Purchases/Create
        public ActionResult Create()
        {
            var purchaseCategories = db.PurchaseCategories.ToList();
            var purchaseEventCategoires = db.PurchaseEventCategories.ToList();

            ViewBag.PurchaseCategories = purchaseCategories;
            ViewBag.PurchaseEventCategoires = purchaseEventCategoires;

            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Place,Amount,NumberOfProducts,UOM,CreatedAt")] Purchase purchase)
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
                    purchase.FileName = serverFileName;
                    purchase.FileOriginalName = filename;
                }

                var formData = Request.Form;
                var purchaseCategoryId = Int32.Parse(formData.Get("PurchaseCategory"));
                var purchaseEventCategory = Int32.Parse(formData.Get("PurchaseEventCategory"));

                purchase.PurchaseCategory = db.PurchaseCategories.Find(purchaseCategoryId);
                purchase.PurchaseEventCategory = db.PurchaseEventCategories.Find(purchaseEventCategory);

                db.Purchases.Add(purchase);
                db.SaveChanges();
                return RedirectToAction("Details", "Purchases", new { id = purchase.ID });
            }

            return View(purchase);
        }

        // GET: Purchases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Place,Amount,NumberOfProducts,UOM,CreatedAt")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            db.Purchases.Remove(purchase);
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
