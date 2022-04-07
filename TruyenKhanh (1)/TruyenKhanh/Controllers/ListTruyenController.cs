using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TruyenKhanh.Models;

namespace TruyenKhanh.Controllers
{
    public class ListTruyenController : Controller
    {
        // GET: ListTruyen
        dbFSDataContext db = new dbFSDataContext();
        public ActionResult ListTruyen()
        {
            var all_truyen = from ss in db.Truyens select ss;
            return View(all_truyen);
        }
        public ActionResult Detail(int id)
        {
            var D_truyen = db.Truyens.Where(m => m.MaTruyen == id).First();
            return View(D_truyen);
        }
    }
}