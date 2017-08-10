using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskUploader.Models;

namespace TaskUploader.Controllers
{
    public class FileUploadController : Controller
    {
        TaskUploaderEntities dc = new TaskUploaderEntities();

        public ActionResult List()
        {
            return View(dc.Articles.ToList());
        }

        public ActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit(Article article, HttpPostedFileBase file)
        {
            if(file!= null)
            {
                //string ds = file.FileName.Substring(file.FileName.Length - 3);
                //string p = string.Empty;

                string fileName = file.FileName;
                string fileExtn = Path.GetExtension(fileName);

                if(fileExtn == ".pdf")
                {
                    //string fileType = file.ContentType;
                    //byte[] fileBytes = new byte[file.ContentLength];
                    //file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    //article.FileLink = fileBytes;
                    //dc.Articles.Add(article);
                    //dc.SaveChanges();

                    ViewBag.UploadOk= "Upload success.";
                }
                else
                {
                    ViewBag.UploadError = "Upload Error.";
                }
            }
            
            return View();
        }
    }
}