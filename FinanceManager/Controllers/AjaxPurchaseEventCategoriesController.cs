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
    public class AjaxPurchaseEventCategoriesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AjaxPurchaseEventCategories
        public IQueryable<PurchaseEventCategory> GetPurchaseEventCategories()
        {
            return db.PurchaseEventCategories;
        }

        // GET: api/AjaxPurchaseEventCategories/5
        [ResponseType(typeof(PurchaseEventCategory))]
        public IHttpActionResult GetPurchaseEventCategory(int id)
        {
            PurchaseEventCategory purchaseEventCategory = db.PurchaseEventCategories.Find(id);
            if (purchaseEventCategory == null)
            {
                return NotFound();
            }

            return Ok(purchaseEventCategory);
        }

        // PUT: api/AjaxPurchaseEventCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchaseEventCategory(int id, PurchaseEventCategory purchaseEventCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchaseEventCategory.ID)
            {
                return BadRequest();
            }

            db.Entry(purchaseEventCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseEventCategoryExists(id))
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

        // POST: api/AjaxPurchaseEventCategories
        [ResponseType(typeof(PurchaseEventCategory))]
        public IHttpActionResult PostPurchaseEventCategory(PurchaseEventCategory purchaseEventCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PurchaseEventCategories.Add(purchaseEventCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchaseEventCategory.ID }, purchaseEventCategory);
        }

        // DELETE: api/AjaxPurchaseEventCategories/5
        [ResponseType(typeof(PurchaseEventCategory))]
        public IHttpActionResult DeletePurchaseEventCategory(int id)
        {
            PurchaseEventCategory purchaseEventCategory = db.PurchaseEventCategories.Find(id);
            if (purchaseEventCategory == null)
            {
                return NotFound();
            }

            db.PurchaseEventCategories.Remove(purchaseEventCategory);
            db.SaveChanges();

            return Ok(purchaseEventCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseEventCategoryExists(int id)
        {
            return db.PurchaseEventCategories.Count(e => e.ID == id) > 0;
        }
    }
}