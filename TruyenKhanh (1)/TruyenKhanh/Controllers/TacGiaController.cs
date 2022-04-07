using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TruyenKhanh.Models;

namespace TruyenKhanh.Controllers
{
    public class TacGiaController : Controller
    {
        // GET: TacGia
        dbFSDataContext db = new dbFSDataContext();

        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                return View(db.TacGias.ToList());
            }
        }


        [HttpGet]
        public ActionResult ThemTacGia()
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemTacGia(TacGia cd, HttpPostedFileBase fileUpload)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                db.TacGias.InsertOnSubmit(cd);
                db.SubmitChanges();
                return RedirectToAction("Index", "TacGia");

            }
        }

        ////Hiển thị sản phẩm
        public ActionResult ChitietTacGia(int id)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                //Lay ra doi tuong sach theo ma
                TacGia cd = db.TacGias.SingleOrDefault(n => n.MaTG == id);
                ViewBag.MaTG = cd.MaTG;
                if (cd == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(cd);
            }
        }

        ////Xóa sản phẩm
        [HttpGet]
        public ActionResult XoaTG(int id)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                //Lay ra doi tuong sach can xoa theo ma
                TacGia cd = db.TacGias.SingleOrDefault(n => n.MaTG == id);
                ViewBag.MaTG = cd.MaTG;
                if (cd == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(cd);
            }
        }

        [HttpPost, ActionName("XoaTG")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                //Lay ra doi tuong sach can xoa theo ma
                TacGia cd = db.TacGias.SingleOrDefault(n => n.MaTG == id);
                ViewBag.MaTG = cd.MaTG;
                if (cd == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                db.TacGias.DeleteOnSubmit(cd);
                db.SubmitChanges();
                return RedirectToAction("Index", "ChuDe");
            }
        }

        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var chude = from cd in db.TacGias where cd.MaTG == id select cd;
                return View(chude.SingleOrDefault());
            }
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Xacnhansua(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                TacGia chude = db.TacGias.SingleOrDefault(n => n.MaTG == id);

                UpdateModel(chude);
                db.SubmitChanges();
                return RedirectToAction("Index", "ChuDe");
            }
        }
    }
}