using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult Upload()
        {
            return View();
        }

        public ActionResult List()
        {
            return View(dc.Articles.ToList());
        }
    }
}