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
using CF__MVC_Project.DAL;
using CF__MVC_Project.Data;

namespace CF__MVC_Project.Controllers
{
    public class bookPurchasesAPIController : ApiController
    {
        private DBContext db = new DBContext();
        // GET: api/bookPurchasesAPI/GetBookPurchaseByBookID?id=1
        [Route("api/bookPurchasesAPI/GetBookPurchaseByBookID/{bookid}")]
        [HttpGet]
        public IHttpActionResult GetBookPurchaseByBookID(int bookid)
        {
            IEnumerable<bookPurchase> item = (from i in db.bookPurchases where i.bookid == bookid orderby i.purchaseID select i).ToList();
            return Ok(item);
        }

        // GET: api/bookPurchasesAPI
        public IQueryable<bookPurchase> GetbookPurchases()
        {
            return db.bookPurchases.AsQueryable();
        }

        // GET: api/bookPurchasesAPI/5
        [ResponseType(typeof(bookPurchase))]
        public IHttpActionResult GetbookPurchase(int id)
        {
            bookPurchase bookPurchase = db.bookPurchases.Find(id);
            if (bookPurchase == null)
            {
                return NotFound();
            }

            return Ok(bookPurchase);
        }

        // PUT: api/bookPurchasesAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutbookPurchase(int id, bookPurchase bookPurchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bookPurchase.purchaseID)
            {
                return BadRequest();
            }

            db.Entry(bookPurchase).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bookPurchaseExists(id))
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

        // POST: api/bookPurchasesAPI
        [ResponseType(typeof(bookPurchase))]
        public IHttpActionResult PostbookPurchase(bookPurchase bookPurchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.bookPurchases.Add(bookPurchase);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bookPurchase.purchaseID }, bookPurchase);
        }

        // DELETE: api/bookPurchasesAPI/5
        [ResponseType(typeof(bookPurchase))]
        public IHttpActionResult DeletebookPurchase(int id)
        {
            bookPurchase bookPurchase = db.bookPurchases.Find(id);
            if (bookPurchase == null)
            {
                return NotFound();
            }

            db.bookPurchases.Remove(bookPurchase);
            db.SaveChanges();

            return Ok(bookPurchase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool bookPurchaseExists(int id)
        {
            return db.bookPurchases.Count(e => e.purchaseID == id) > 0;
        }
    }
}