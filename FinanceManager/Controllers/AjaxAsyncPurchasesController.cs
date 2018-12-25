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
using FinanceManager.Models.RequestReceivers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FinanceManager.Controllers
{
    public class AjaxAsyncPurchasesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AjaxAsyncPurchases
        public IQueryable<Purchase> GetPurchases()
        {
            return db.Purchases;
        }

        // GET: api/AjaxAsyncPurchases/5
        [ResponseType(typeof(Purchase))]
        public IHttpActionResult GetPurchase(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return NotFound();
            }

            return Ok(purchase);
        }

        // PUT: api/AjaxAsyncPurchases/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchase(int id, Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchase.ID)
            {
                return BadRequest();
            }

            db.Entry(purchase).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseExists(id))
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

        // POST: api/AjaxAsyncPurchases
        [ResponseType(typeof(Purchase))]
        public IHttpActionResult PostPurchase([FromBody] JObject data)
        {
            var receiver = data.ToObject<PurchaseCreateReceiver>();
            //var tmp = JsonConvert.DeserializeObject<PurchaseCreateReceiver>(data.ToString());
            var purchase = receiver.CastToPurchase();

            if (receiver.PurchaseCategory != 0)
                purchase.PurchaseCategory = db.PurchaseCategories.Find(receiver.PurchaseCategory);

            if (receiver.PurchaseEventCategory != 0)
                purchase.PurchaseEventCategory = db.PurchaseEventCategories.Find(receiver.PurchaseEventCategory);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Purchases.Add(purchase);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchase.ID }, purchase);
        }

        // DELETE: api/AjaxAsyncPurchases/5
        [ResponseType(typeof(Purchase))]
        public IHttpActionResult DeletePurchase(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return NotFound();
            }

            db.Purchases.Remove(purchase);
            db.SaveChanges();

            return Ok(purchase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseExists(int id)
        {
            return db.Purchases.Count(e => e.ID == id) > 0;
        }
    }
}