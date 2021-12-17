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
using CF__MVC_Project.Models;

namespace CF__MVC_Project.Controllers
{
    public class booksAPIController : ApiController
    {
        private DBContext db = new DBContext();

        // GET: api/booksAPIController/GetBooksByCategory/OOP

        //[HttpGet]
        //public IEnumerable<book> GetBooksByCategory(string id)
        //{
        //    IEnumerable<book> item = (from i in db.books where i.categoriesTBcategory == id orderby i.id select i).ToList();
        //    return item;
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

        // GET: api/booksAPI/5
        [ResponseType(typeof(book))]
        public IHttpActionResult Getbook(int id)
        {
            book book = db.books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/booksAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putbook(int id, book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.id)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bookExists(id))
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

        // POST: api/booksAPI
        [ResponseType(typeof(book))]
        public IHttpActionResult Postbook(book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.books.Add(book);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book.id }, book);
        }

        // DELETE: api/booksAPI/5
        [ResponseType(typeof(book))]
        public IHttpActionResult Deletebook(int id)
        {
            book book = db.books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.books.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool bookExists(int id)
        {
            return db.books.Count(e => e.id == id) > 0;
        }
    }
}