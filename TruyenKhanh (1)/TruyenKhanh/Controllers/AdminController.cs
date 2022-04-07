using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TruyenKhanh.Models;

namespace TruyenKhanh.Controllers
{
    public class AdminController : Controller
    {
        dbFSDataContext db = new dbFSDataContext();
        // GET: Admin


        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                return View();
            }
        }
        public ActionResult Truyen()
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            { 

                return View(db.Truyens.ToList().OrderBy(n => n.MaTruyen));
            }
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            // Gán các giá trị người dùng nhập liệu cho các biến 
            var tendn = collection["IDadmin"];
            var matkhau = collection["Pass"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                //Gán giá trị cho đối tượng được tạo mới (ad)        

                QTV ad = db.QTVs.SingleOrDefault(n => n.IDadmin == tendn && n.Pass == matkhau);
                if (ad != null)
                {
                    // ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Themsp()
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {


                ViewBag.MaTG = new SelectList(db.TacGias.ToList().OrderBy(n => n.TenTG), "MaTG", "TenTG");
                ViewBag.MaYT = new SelectList(db.TruyenYeuThiches.ToList().OrderBy(n => n.TenYT), "MaYT", "TenYT");
                ViewBag.MaTL = new SelectList(db.TheLoais.ToList().OrderBy(n => n.TenTL), "MaTL", "TenTL");
                return View();
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemSp(Truyen sp, HttpPostedFileBase fileUpload)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {

                ViewBag.MaTG = new SelectList(db.TacGias.ToList().OrderBy(n => n.TenTG), "MaTG", "TenTG");
                ViewBag.MaYT = new SelectList(db.TruyenYeuThiches.ToList().OrderBy(n => n.TenYT), "MaYT", "TenYT");
                ViewBag.MaTL = new SelectList(db.TheLoais.ToList().OrderBy(n => n.TenTL), "MaTL", "TenTL");

                if (fileUpload == null)
                {
                    ViewBag.Thongbao = "Vui lòng chọn ảnh";
                    return View();
                }
                else
                {
                    if (ModelState.IsValid)
                    {

                        var fileName = Path.GetFileName(fileUpload.FileName);

                        var path = Path.Combine(Server.MapPath("~/Images/SPkhuyenmai"), fileName);

                        if (System.IO.File.Exists(path))
                            ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        else
                        {

                            fileUpload.SaveAs(path);
                        }
                        sp.Hinh = fileName;

                        db.Truyens.InsertOnSubmit(sp);
                        db.SubmitChanges();
                    }
                    return RedirectToAction("Index", "Admin");
                }
            }
        }

       
        
        ////Xóa sản phẩm
        [HttpGet]
        public ActionResult Xoasp(int id)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                //Lay ra doi tuong sach can xoa theo ma
                Truyen sp = db.Truyens.SingleOrDefault(n => n.MaTruyen == id);
                ViewBag.MaTruyen = sp.MaTruyen;
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(sp);
            }
        }

        [HttpPost, ActionName("Xoasp")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                //Lay ra doi tuong sach can xoa theo ma
                Truyen sp = db.Truyens.SingleOrDefault(n => n.MaTruyen == id);
                ViewBag.MaTruyen = sp.MaTruyen;
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                db.Truyens.DeleteOnSubmit(sp);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Suasp(int id)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {

                Truyen sp = db.Truyens.SingleOrDefault(n => n.MaTruyen == id);
                ViewBag.MaTruyen = sp.MaTruyen;
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }


                ViewBag.MaTG = new SelectList(db.TacGias.ToList().OrderBy(n => n.TenTG), "MaTG", "TenTG");
                ViewBag.MaYT = new SelectList(db.TruyenYeuThiches.ToList().OrderBy(n => n.TenYT), "MaYT", "TenYT");
                ViewBag.MaTL = new SelectList(db.TheLoais.ToList().OrderBy(n => n.TenTL), "MaTL", "TenTL");
                return View(sp);
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suasp(int id, HttpPostedFileBase fileUpload)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {

                ViewBag.MaTG = new SelectList(db.TacGias.ToList().OrderBy(n => n.TenTG), "MaTG", "TenTG");
                ViewBag.MaYT = new SelectList(db.TruyenYeuThiches.ToList().OrderBy(n => n.TenYT), "MaYT", "TenYT");
                ViewBag.MaTL = new SelectList(db.TheLoais.ToList().OrderBy(n => n.TenTL), "MaTL", "TenTL");

                if (fileUpload == null)
                {
                    ViewBag.Thongbao = "Vui lòng chọn ảnh";
                    return View();
                }
                else
                {
                    Truyen sp = db.Truyens.SingleOrDefault(n => n.MaTruyen == id);
                    if (ModelState.IsValid)
                    {

                        var fileName = Path.GetFileName(fileUpload.FileName);

                        var path = Path.Combine(Server.MapPath("~/Images/SPkhuyenmai"), fileName);

                        if (System.IO.File.Exists(path))
                            ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        else
                        {

                            fileUpload.SaveAs(path);
                        }

                        sp.Hinh = fileName;

                        UpdateModel(sp);
                        db.SubmitChanges();
                    }
                    return RedirectToAction("Index", "Admin");
                }
            }
        }



    }
}