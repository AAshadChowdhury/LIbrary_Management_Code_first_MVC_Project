using CF__MVC_Project.DAL;
using CF__MVC_Project.Data;
using CF__MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CF__MVC_Project.Controllers
{  //[Authorize]
    public class BooksController : Controller
    {
       
        
            DBContext db = new DBContext();

            // GET: Books
            [AllowAnonymous]
            public ActionResult Index()
            {
                List<bookVM> BookVMlist = new List<bookVM>();
                var AllBooks = (from book in db.books select new { book.id, book.name, book.author, book.publisher, book.categoriesTB.category, book.stockQuantity, book.price, book.cover }).ToList();
                foreach (var item in AllBooks)
                {
                    bookVM bookvm = new bookVM();

                    bookvm.id = item.id;
                    bookvm.name = item.name;
                    bookvm.author = item.author;
                    bookvm.publisher = item.publisher;
                    bookvm.category = item.category;
                    bookvm.stockQuantity = item.stockQuantity;
                    bookvm.price = item.price;
                    bookvm.cover = item.cover;

                    BookVMlist.Add(bookvm);
                }
                return View(BookVMlist);
            }


            public ActionResult Edit(int id)
            {
                List<DAL.categoriesTB> item = null;
                item = (from c in db.categoriesTBs select c).ToList();
                ViewBag.ddlCategories = new SelectList(item, "category", "category");
                //return View();


                bookVM bookvm = new bookVM();
                book book = db.books.Find(id);

                bookvm.id = book.id;
                bookvm.name = book.name;
                bookvm.author = book.author;
                bookvm.publisher = book.publisher;
                bookvm.category = book.categoriesTBcategory;
                bookvm.stockQuantity = book.stockQuantity;
                bookvm.price = book.price;
                bookvm.cover = book.cover;


                return View(bookvm);
            }
            //[HttpParamAction]
            [HttpPost]
            public ActionResult Edit(bookVM bookvm)
            {
                #region old code

                string FileName = Path.GetFileName(bookvm.ImageFile.FileName);
                string FilePath = Path.Combine(Server.MapPath("~/coverimage"), FileName);


                book book = db.books.Find(bookvm.id);

                //book.id = bookvm.id;
                book.name = bookvm.name;
                book.author = bookvm.author;
                book.publisher = bookvm.publisher;
                book.categoriesTBcategory = bookvm.category;
                book.stockQuantity = bookvm.stockQuantity;
                book.price = bookvm.price;
                //book.cover = bookvm.cover;
                book.cover = bookvm.ImageFile.FileName;


                ViewBag.Check = "true";
                try
                {
                    book item = (from c in db.books where c.id == bookvm.id select c).FirstOrDefault();
                    if (item == null)
                    {
                        ViewBag.error = String.Format("Item with id {0} was not found", bookvm.id);
                    }
                    bool ok = TryUpdateModel(book);
                    try
                    {
                        int a = db.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        string b = ex.Message;
                    }

                    if (bookvm.ImageFile.FileName != null)
                    {
                        //string FileName = Path.GetFileName(bookvm.ImageFile.FileName);
                        //string FilePath = Path.Combine(Server.MapPath("~/coverimage"), FileName);
                        bookvm.ImageFile.SaveAs(FilePath);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.error = ex.Message.ToString();

                }
                //return View("index");
                return RedirectToAction("Index");
                #endregion

                book book1 = db.books.Find(bookvm.id);
                book1.id = bookvm.id;
                book1.name = bookvm.name;
                book1.author = bookvm.author;
                book1.publisher = bookvm.publisher;
                book1.categoriesTBcategory = bookvm.category;
                book1.stockQuantity = bookvm.stockQuantity;
                book1.price = bookvm.price;
                //book.cover = bookvm.cover;
                book1.cover = bookvm.ImageFile.FileName;

                TryUpdateModel(book1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            public ActionResult Create()
            {
                List<DAL.categoriesTB> item = null;
                item = (from c in db.categoriesTBs select c).ToList();
                ViewBag.ddlCategories = new SelectList(item, "category", "category");
                return View();
            }
            //[HttpParamAction]
            [HttpPost]
            public ActionResult Create(bookVM bookvm)
            {
                string FileName = Path.GetFileName(bookvm.ImageFile.FileName);
                string FilePath = Path.Combine(Server.MapPath("~/coverimage"), FileName);
                book book = new book();

                //book.id = bookvm.id;
                book.name = bookvm.name;
                book.author = bookvm.author;
                book.publisher = bookvm.publisher;
                book.categoriesTBcategory = bookvm.category;
                book.stockQuantity = bookvm.stockQuantity;
                book.price = bookvm.price;
                //book.cover = bookvm.cover;
                book.cover = FileName;

                ViewBag.Check = "true";
                try
                {
                    db.books.Add(book);
                    db.SaveChanges();
                    bookvm.ImageFile.SaveAs(FilePath);
                }
                catch (Exception ex)
                {
                    ViewBag.error = ex.Message.ToString();
                }
                //return View("index");
                return RedirectToAction("Index");

            }

            public ActionResult Details(int id)
            {
                bookVM bookvm = new bookVM();
                book book = db.books.Find(id);

                bookvm.id = book.id;
                bookvm.name = book.name;
                bookvm.author = book.author;
                bookvm.publisher = book.publisher;
                bookvm.category = book.categoriesTBcategory;
                bookvm.stockQuantity = book.stockQuantity;
                bookvm.price = book.price;
                bookvm.cover = book.cover;

                return View(bookvm);
            }

            [HttpPost, ActionName("Details")]
            public ActionResult Delete(int id)
            {
                //int id = bookvm.id;
                book book = db.books.Find(id);
                db.books.Remove(book);
                int a = db.SaveChanges();
                return RedirectToAction("Index");
            }



            [HttpPost, ActionName("Index")]
            public ActionResult Delete1(int id)
            {
                //int id = bookvm.id;
                book book = db.books.Find(id);
                db.books.Remove(book);
                int a = db.SaveChanges();
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