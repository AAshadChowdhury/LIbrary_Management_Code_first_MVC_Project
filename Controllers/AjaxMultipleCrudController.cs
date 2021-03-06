using CF__MVC_Project.DAL;
using CF__MVC_Project.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CF__MVC_Project.Controllers
{
    public class AjaxMultipleCrudController : Controller
    {
        private DBContext db = new DBContext();

        // GET: AjaxMultipleCrud

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryAndBooks()
        {
            List<categoriesTB> item = null;
            
            item = (from c in db.categoriesTBs select c).ToList();
            ViewBag.ddlCategories = new SelectList(item, "category", "category");

            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";
                        //string filename = Path.GetFileName(Request.Files[i].FileName);

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.
                        fname = Path.Combine(Server.MapPath("~/coverimage/"), fname);
                        file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
    }
}