using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskUploader.Models;

namespace TaskUploader.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User userRegister)
        {
            using (TaskUploaderEntities dbModel = new TaskUploaderEntities())
            {
                if(dbModel.Users.Any(x=>x.UserName == userRegister.UserName))
                {
                    ViewBag.UserExist = "Username already exist";
                    return View("Register", userRegister);
                }

                dbModel.Users.Add(userRegister);
                dbModel.SaveChanges();

                ViewBag.RegisterSuccess = "Your registration is successful.";
            }

            ModelState.Clear();

            return View("Register", new User());
        }

        public ActionResult Login()
        {
            return View();
        }

        private TaskUploaderEntities db = new TaskUploaderEntities();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User userLogin)
        {

            if (ModelState.IsValid)
            {
                var check = from userList in db.Users
                            where userList.UserName == userLogin.UserName && userList.Password == userLogin.Password
                            select userList;

                if(check.FirstOrDefault() != null)
                {
                    return RedirectToAction("Upload", "UploadFile");
                }else
                {
                    ViewBag.LoginError = "Invalid username or password.";
                }
            }

            return View(userLogin);
        }

        public ActionResult Welcome()
        {
            return View();
        }

    }
}