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
    public class categoriesTBsAPIController : ApiController
    {
        private DBContext db = new DBContext();

        // GET: api/categoriesTBsAPI
        public IQueryable<categoriesTB> GetcategoriesTBs()
        {
            return db.categoriesTBs;
        }

        // GET: api/categoriesTBsAPI/5
        [ResponseType(typeof(categoriesTB))]
        public IHttpActionResult GetcategoriesTB(string id)
        {
            categoriesTB categoriesTB = db.categoriesTBs.Find(id);
            if (categoriesTB == null)
            {
                return NotFound();
            }

            return Ok(categoriesTB);
        }

        // PUT: api/categoriesTBsAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutcategoriesTB(string id, categoriesTB categoriesTB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoriesTB.category)
            {
                return BadRequest();
            }

            db.Entry(categoriesTB).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!categoriesTBExists(id))
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

        // POST: api/categoriesTBsAPI
        [ResponseType(typeof(categoriesTB))]
        public IHttpActionResult PostcategoriesTB(categoriesTB categoriesTB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.categoriesTBs.Add(categoriesTB);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (categoriesTBExists(categoriesTB.category))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = categoriesTB.category }, categoriesTB);
        }

        // DELETE: api/categoriesTBsAPI/5
        [ResponseType(typeof(categoriesTB))]
        public IHttpActionResult DeletecategoriesTB(string id)
        {
            categoriesTB categoriesTB = db.categoriesTBs.Find(id);
            if (categoriesTB == null)
            {
                return NotFound();
            }

            db.categoriesTBs.Remove(categoriesTB);
            db.SaveChanges();

            return Ok(categoriesTB);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool categoriesTBExists(string id)
        {
            return db.categoriesTBs.Count(e => e.category == id) > 0;
        }
    }
}