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
    public class bookSalesAPIController : ApiController
    {
        private DBContext db = new DBContext();

        //// GET: api/bookSalesAPI/GetBookSalesByBookID?id=1
        //[Route("api/bookSalesAPI/GetBookSaleByBookID/{bookid}")]
        //[HttpGet]
        //public IHttpActionResult GetBookSaleByBookID(int bookid)
        //{
        //    IEnumerable<bookSale> item = (from i in db.bookSales where i.bookid == bookid orderby i.saleID select i).ToList();
        //    return Ok(item);
        //}

        // GET: api/booksAPI
        public IEnumerable<book> Getbooks(string categoriesTBcategory = "")
        {
            if (categoriesTBcategory != "")
            {
                IEnumerable<book> item = (from i in db.books where i.categoriesTBcategory == categoriesTBcategory orderby i.id select i).ToList();
                return item;
            }
            return db.books;
        }








        // GET: api/bookSalesAPI
        public IEnumerable<bookSale> GetbookSales(int bookid)
        {
            if (bookid != null)
            {
                IEnumerable<bookSale> item = (from i in db.bookSales where i.bookid == bookid orderby i.saleID select i).ToList();
                return item;
            }
            return db.bookSales;
        }

        // GET: api/bookSalesAPI/5
        [ResponseType(typeof(bookSale))]
        public IHttpActionResult GetbookSale(int id)
        {
            bookSale bookSale = db.bookSales.Find(id);
            if (bookSale == null)
            {
                return NotFound();
            }

            return Ok(bookSale);
        }

        // PUT: api/bookSalesAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutbookSale(int id, bookSale bookSale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bookSale.saleID)
            {
                return BadRequest();
            }

            db.Entry(bookSale).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bookSaleExists(id))
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

        // POST: api/bookSalesAPI
        [ResponseType(typeof(bookSale))]
        public IHttpActionResult PostbookSale(bookSale bookSale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.bookSales.Add(bookSale);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bookSale.saleID }, bookSale);
        }

        // DELETE: api/bookSalesAPI/5
        [ResponseType(typeof(bookSale))]
        public IHttpActionResult DeletebookSale(int id)
        {
            bookSale bookSale = db.bookSales.Find(id);
            if (bookSale == null)
            {
                return NotFound();
            }

            db.bookSales.Remove(bookSale);
            db.SaveChanges();

            return Ok(bookSale);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool bookSaleExists(int id)
        {
            return db.bookSales.Count(e => e.saleID == id) > 0;
        }
    }
}