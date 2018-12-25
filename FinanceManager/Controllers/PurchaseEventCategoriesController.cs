using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinanceManager.Models;

namespace FinanceManager.Controllers
{
    public class PurchaseEventCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PurchaseEventCategories
        public ActionResult Index()
        {
            return View(db.PurchaseEventCategories.ToList());
        }

        // GET: PurchaseEventCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseEventCategory purchaseEventCategory = db.PurchaseEventCategories.Find(id);
            if (purchaseEventCategory == null)
            {
                return HttpNotFound();
            }
            return View(purchaseEventCategory);
        }

        // GET: PurchaseEventCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PurchaseEventCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,StartingDate")] PurchaseEventCategory purchaseEventCategory)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseEventCategories.Add(purchaseEventCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(purchaseEventCategory);
        }

        // GET: PurchaseEventCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseEventCategory purchaseEventCategory = db.PurchaseEventCategories.Find(id);
            if (purchaseEventCategory == null)
            {
                return HttpNotFound();
            }
            return View(purchaseEventCategory);
        }

        // POST: PurchaseEventCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,StartingDate")] PurchaseEventCategory purchaseEventCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseEventCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchaseEventCategory);
        }

        // GET: PurchaseEventCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseEventCategory purchaseEventCategory = db.PurchaseEventCategories.Find(id);
            if (purchaseEventCategory == null)
            {
                return HttpNotFound();
            }
            return View(purchaseEventCategory);
        }

        // POST: PurchaseEventCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseEventCategory purchaseEventCategory = db.PurchaseEventCategories.Find(id);
            db.PurchaseEventCategories.Remove(purchaseEventCategory);
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
