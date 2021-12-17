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
    public class bookSalesController : Controller
    {
        private DBContext db = new DBContext();














        // GET: bookSales
        public ActionResult Index()
        {
            var bookSales = db.bookSales.Include(b => b.book);
            return View(bookSales.ToList());
        }

        // GET: bookSales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookSale bookSale = db.bookSales.Find(id);
            if (bookSale == null)
            {
                return HttpNotFound();
            }
            return View(bookSale);
        }

        // GET: bookSales/Create
        public ActionResult Create()
        {
            ViewBag.bookid = new SelectList(db.books, "id", "name");
            return View();
        }

        // POST: bookSales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "saleID,bookid,quantity,saleDate")] bookSale bookSale)
        {
            if (ModelState.IsValid)
            {
                db.bookSales.Add(bookSale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bookid = new SelectList(db.books, "id", "name", bookSale.bookid);
            return View(bookSale);
        }

        // GET: bookSales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookSale bookSale = db.bookSales.Find(id);
            if (bookSale == null)
            {
                return HttpNotFound();
            }
            ViewBag.bookid = new SelectList(db.books, "id", "name", bookSale.bookid);
            return View(bookSale);
        }

        // POST: bookSales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "saleID,bookid,quantity,saleDate")] bookSale bookSale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookSale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bookid = new SelectList(db.books, "id", "name", bookSale.bookid);
            return View(bookSale);
        }

        // GET: bookSales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookSale bookSale = db.bookSales.Find(id);
            if (bookSale == null)
            {
                return HttpNotFound();
            }
            return View(bookSale);
        }

        // POST: bookSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bookSale bookSale = db.bookSales.Find(id);
            db.bookSales.Remove(bookSale);
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
