using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskUploader.Models;

namespace TaskUploader.Controllers
{
    public class UploadFileController : Controller
    {
        TaskUploaderEntities dc = new TaskUploaderEntities();

        public ActionResult Index()
        {
            return View();
        }
        private bool isPdfFile(string fileExtension)
        {
            return fileExtension.Equals("pdf");
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(Article article, HttpPostedFileBase pdfFile)
        {
            string ext = Path.GetExtension(pdfFile.FileName);

            //handling for pdf only
            if (pdfFile != null)
            {
                if (ext == ".pdf")
                {

                    Article toSaveToDB = new Article();

                    var fileName = Path.GetFileName(pdfFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/Files/"), fileName);

                    //article.FileName = pdfFile.
                    article.FileLink = fileName;

                    dc.Articles.Add(article);
                    dc.SaveChanges();

                    pdfFile.SaveAs(path);
                    ViewBag.UploadSuccess = "Upload success.";
                }
                else
                {
                    ViewBag.ExtensionError = "Only PDF files are allowed.";
                }
            }
            else
            {
                ViewBag.UploadError = "Something went wrong. Upload failed.";
            }

            return View();
        
        }

        public ActionResult List()
        {
            return View(dc.Articles.ToList());
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = dc.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = dc.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Article article)
        {
            if (ModelState.IsValid)
            {
                dc.Entry(article).State = EntityState.Modified;
                dc.SaveChanges();
                return RedirectToAction("List");
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = dc.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = dc.Articles.Find(id);
            dc.Articles.Remove(article);
            dc.SaveChanges();
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dc.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}