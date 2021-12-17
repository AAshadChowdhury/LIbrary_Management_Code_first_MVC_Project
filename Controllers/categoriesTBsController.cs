using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CF__MVC_Project.DAL;
using CF__MVC_Project.Data;

namespace CF__MVC_Project.Controllers
{
    public class categoriesTBsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: categoriesTBs
        public ActionResult Index()
        {
            return View(db.categoriesTBs.ToList());
        }

        // GET: categoriesTBs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categoriesTB categoriesTB = db.categoriesTBs.Find(id);
            if (categoriesTB == null)
            {
                return HttpNotFound();
            }
            return View(categoriesTB);
        }

        // GET: categoriesTBs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: categoriesTBs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "category,categoryInfo")] categoriesTB categoriesTB)
        {
            if (ModelState.IsValid)
            {
                db.categoriesTBs.Add(categoriesTB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoriesTB);
        }

        // GET: categoriesTBs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categoriesTB categoriesTB = db.categoriesTBs.Find(id);
            if (categoriesTB == null)
            {
                return HttpNotFound();
            }
            return View(categoriesTB);
        }

        // POST: categoriesTBs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "category,categoryInfo")] categoriesTB categoriesTB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoriesTB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoriesTB);
        }

        // GET: categoriesTBs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categoriesTB categoriesTB = db.categoriesTBs.Find(id);
            if (categoriesTB == null)
            {
                return HttpNotFound();
            }
            return View(categoriesTB);
        }

        // POST: categoriesTBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            categoriesTB categoriesTB = db.categoriesTBs.Find(id);
            db.categoriesTBs.Remove(categoriesTB);
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
