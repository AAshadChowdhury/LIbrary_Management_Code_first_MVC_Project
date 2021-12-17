using CF__MVC_Project.DAL;
using CF__MVC_Project.Data;
using CF__MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CF__MVC_Project.Controllers
{
    public class AjaxBookPurchaseController : Controller
    {
        private DBContext db = new DBContext();
        // GET: AjaxBookPurchase
        public ActionResult Index()
        {
            List<book> item = null;
            item = (from c in db.books select c).ToList();
            ViewBag.ddlBooks = new SelectList(item, "id", "name");
            return View();
        }
    }
}