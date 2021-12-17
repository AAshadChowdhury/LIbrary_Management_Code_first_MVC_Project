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
    public class bookPurchasesController : Controller
    {
        private DBContext db = new DBContext();

        // GET: bookPurchases
        public ActionResult Index()
        {
            var bookPurchases = db.bookPurchases.Include(b => b.book);
            return View(bookPurchases.ToList());
        }

        // GET: bookPurchases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookPurchase bookPurchase = db.bookPurchases.Find(id);
            if (bookPurchase == null)
            {
                return HttpNotFound();
            }
            return View(bookPurchase);
        }

        // GET: bookPurchases/Create
        public ActionResult Create()
        {
            ViewBag.bookid = new SelectList(db.books, "id", "name");
            return View();
        }

        // POST: bookPurchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "purchaseID,bookid,quantity,purchaseDate")] bookPurchase bookPurchase)
        {
            if (ModelState.IsValid)
            {
                db.bookPurchases.Add(bookPurchase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bookid = new SelectList(db.books, "id", "name", bookPurchase.bookid);
            return View(bookPurchase);
        }

        // GET: bookPurchases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookPurchase bookPurchase = db.bookPurchases.Find(id);
            if (bookPurchase == null)
            {
                return HttpNotFound();
            }
            ViewBag.bookid = new SelectList(db.books, "id", "name", bookPurchase.bookid);
            return View(bookPurchase);
        }

        // POST: bookPurchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "purchaseID,bookid,quantity,purchaseDate")] bookPurchase bookPurchase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookPurchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bookid = new SelectList(db.books, "id", "name", bookPurchase.bookid);
            return View(bookPurchase);
        }

        // GET: bookPurchases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookPurchase bookPurchase = db.bookPurchases.Find(id);
            if (bookPurchase == null)
            {
                return HttpNotFound();
            }
            return View(bookPurchase);
        }

        // POST: bookPurchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bookPurchase bookPurchase = db.bookPurchases.Find(id);
            db.bookPurchases.Remove(bookPurchase);
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
