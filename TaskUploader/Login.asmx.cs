using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using TaskUploader.Models;

namespace TaskUploader
{
    /// <summary>
    /// Summary description for Login
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Login : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        TaskUploaderEntities db = new TaskUploaderEntities();

        [WebMethod]
        public string UserLogin(string username, string password)
        {
            User usr = new User();
            if (username == usr.UserName && password == usr.Password)
            {
                return "loginSuccess";
            }

            return "loginFailed";
        }
    }
}
