using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssistMVC2018.Models;

namespace CommunityAssistMVC2018.Controllers
{
    public class GrantController : Controller
    {
        CommunityAssist2017Entities db = new CommunityAssist2017Entities();

        // GET: Grant
        public ActionResult Index()

        {
            if (Session["userKey"] == null)
            {
                Message msg = new Message("Must be logged in to apply for a grant!");
                return RedirectToAction("Result", msg);

            }
            ViewBag.GrantTypeKey = new SelectList(db.GrantTypes, "GrantTypeKey", "GrantTypeName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "GrantType, PersonKey , GrantApplicationDate, GrantApplicationReason, GrantApplicationRequestAmount  ")]GrantApplication g)
        {

            g.PersonKey = (int)Session["userkey"];
            g.GrantApplicationStatusKey = 1;
            g.GrantAppicationDate = DateTime.Now;
            db.GrantApplications.Add(g);
            db.SaveChanges(); 


            Message msg = new Message("Thank you for the applying for a grant!");
            return View("Result", msg);

        }
        public ActionResult Result(Message msg)
        {
            return View(msg);
        }
    }
}