using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TruyenKhanh.Models;

namespace TruyenKhanh.Controllers
{
    public class YeuThichController : Controller
    {
        // GET: YeuThich
        dbFSDataContext data = new dbFSDataContext();

        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View(data.TruyenYeuThiches.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View();
        }
        [HttpPost]
        public ActionResult Create(TruyenYeuThich mausac)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                data.TruyenYeuThiches.InsertOnSubmit(mausac);
                data.SubmitChanges();
                return RedirectToAction("Index", "YeuThich");
            }
        }

        public ActionResult Details(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var mausac = from ms in data.TruyenYeuThiches where ms.MaYT == id select ms;
                return View(mausac.SingleOrDefault());
            }
        }
        [HttpGet]
        public ActionResult XoaYT(int id)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                //Lay ra doi tuong sach can xoa theo ma
                TruyenYeuThich mau = data.TruyenYeuThiches.SingleOrDefault(n => n.MaYT == id);
                ViewBag.MaMau = mau.MaYT;
                if (mau == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(mau);
            }
        }

        [HttpPost, ActionName("XoaYT")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                //Lay ra doi tuong sach can xoa theo ma
                TruyenYeuThich mau = data.TruyenYeuThiches.SingleOrDefault(n => n.MaYT == id);
                ViewBag.MaMau = mau.MaYT;
                if (mau == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                data.TruyenYeuThiches.DeleteOnSubmit(mau);
                data.SubmitChanges();
                return RedirectToAction("Index", "MauSac");
            }
        }


        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var mausac = from ms in data.TruyenYeuThiches where ms.MaYT == id select ms;
                return View(mausac.SingleOrDefault());
            }
        }
        //Do tên Action trùng tên, nên cần tên bí doanh
        [HttpPost, ActionName("Edit")]
        public ActionResult Xacnhansua(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                TruyenYeuThich mausac = data.TruyenYeuThiches.SingleOrDefault(n => n.MaYT == id);

                UpdateModel(mausac);
                data.SubmitChanges();
                return RedirectToAction("Index", "YeuThich");
            }
        }
    }
}