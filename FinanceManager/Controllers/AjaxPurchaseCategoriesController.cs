using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FinanceManager.Models;

namespace FinanceManager.Controllers
{
    public class AjaxPurchaseCategoriesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AjaxPurchaseCategories
        public IQueryable<PurchaseCategory> GetPurchaseCategories()
        {
            return db.PurchaseCategories;
        }

        // GET: api/AjaxPurchaseCategories/5
        [ResponseType(typeof(PurchaseCategory))]
        public IHttpActionResult GetPurchaseCategory(int id)
        {
            PurchaseCategory purchaseCategory = db.PurchaseCategories.Find(id);
            if (purchaseCategory == null)
            {
                return NotFound();
            }

            return Ok(purchaseCategory);
        }

        // PUT: api/AjaxPurchaseCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchaseCategory(int id, PurchaseCategory purchaseCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchaseCategory.ID)
            {
                return BadRequest();
            }

            db.Entry(purchaseCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AjaxPurchaseCategories
        [ResponseType(typeof(PurchaseCategory))]
        public IHttpActionResult PostPurchaseCategory(PurchaseCategory purchaseCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PurchaseCategories.Add(purchaseCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchaseCategory.ID }, purchaseCategory);
        }

        // DELETE: api/AjaxPurchaseCategories/5
        [ResponseType(typeof(PurchaseCategory))]
        public IHttpActionResult DeletePurchaseCategory(int id)
        {
            PurchaseCategory purchaseCategory = db.PurchaseCategories.Find(id);
            if (purchaseCategory == null)
            {
                return NotFound();
            }

            db.PurchaseCategories.Remove(purchaseCategory);
            db.SaveChanges();

            return Ok(purchaseCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseCategoryExists(int id)
        {
            return db.PurchaseCategories.Count(e => e.ID == id) > 0;
        }
    }
}