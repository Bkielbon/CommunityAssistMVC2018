using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssistMVC2018.Models; 

namespace CommunityAssistMVC2018.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "UserName , Password")]LoginClass lc)
        {
            CommunityAssist2017Entities db = new CommunityAssist2017Entities();
            int loginresult = db.usp_Login(lc.UserName, lc.Password);
            if(loginresult !=-1)
            {
                var uid = (from r in db.People
                          where r.PersonEmail.Equals(lc.UserName)
                          select r.PersonKey).FirstOrDefault();

                int Key = (int)uid;
                Session["userKey"] = Key;

                Message msg = new Message("Welcome, " + lc.UserName + ". You can now Donate or apply for a assistance.");
                return RedirectToAction("Result", msg);
            }
            Message message = new Message("Invalid Login");
            return View("Result", message);
        }
        public ActionResult Result(Message msg)
        {
            return View(msg);
        }

    }
}