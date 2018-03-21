using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssistMVC2018.Models;

namespace CommunityAssistMVC2018.Controllers
{
    public class NewDonationController : Controller

    {
        CommunityAssist2017Entities db = new CommunityAssist2017Entities();

        // GET: NewDonation
        public ActionResult Index()
        {
            if (Session["userKey"] == null)
            {
                Message msg = new Message("Must be logged in to donate!");
                return RedirectToAction("Result", msg);

            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "DonationAmount, userkey, DonationDate")]Donation d)
        {
            
            d.PersonKey = (int)Session["userkey"];
            d.DonationDate = DateTime.Now;
            d.DonationConfirmationCode = Guid.NewGuid();
            db.SaveChanges();

            Message msg = new Message("Thanks for the donation");
            return View("Result", msg);

        }
        public ActionResult Result(Message msg)
        {
            return View(msg);
        }

    }
}