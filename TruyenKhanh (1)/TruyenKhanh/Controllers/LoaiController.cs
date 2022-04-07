using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TruyenKhanh.Models;

namespace TruyenKhanh.Controllers
{
    public class LoaiController : Controller
    {
        // GET: Loai
        dbFSDataContext data = new dbFSDataContext();

        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View(data.TheLoais.ToList());
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
        public ActionResult Create(TheLoai loai)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                data.TheLoais.InsertOnSubmit(loai);
                data.SubmitChanges();
                return RedirectToAction("Index", "Loai");
            }
        }

        public ActionResult Details(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var loai = from lsp in data.TheLoais where lsp.MaTL == id select lsp;
                return View(loai.SingleOrDefault());
            }
        }

        [HttpGet]
        public ActionResult XoaLoai(int id)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                //Lay ra doi tuong sach can xoa theo ma
                TheLoai loai = data.TheLoais.SingleOrDefault(n => n.MaTL == id);
                ViewBag.MaLoai = loai.MaTL;
                if (loai == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(loai);
            }
        }

        [HttpPost, ActionName("XoaLoai")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                //Lay ra doi tuong sach can xoa theo ma
                TheLoai loai = data.TheLoais.SingleOrDefault(n => n.MaTL == id);
                if (loai != null)
                    ViewBag.MaLoai = loai.MaTL;
                if (loai == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                data.TheLoais.DeleteOnSubmit(loai);
                data.SubmitChanges();
                return RedirectToAction("Index", "Loai");
            }
        }

        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var loai = from lsp in data.TheLoais where lsp.MaTL == id select lsp;
                return View(loai.SingleOrDefault());
            }
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Xacnhansua(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                TheLoai loai = data.TheLoais.SingleOrDefault(n => n.MaTL == id);

                UpdateModel(loai);
                data.SubmitChanges();
                return RedirectToAction("Index", "Loai");
            }
        }
    }
}