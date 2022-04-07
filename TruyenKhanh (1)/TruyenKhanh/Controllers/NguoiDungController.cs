using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TruyenKhanh.Models;

namespace TruyenKhanh.Controllers
{
    public class NguoiDungController : Controller
    {
        dbFSDataContext db = new dbFSDataContext();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]

        public ActionResult DangKy(FormCollection collection, NguoiDung kh)
        {

            var tendn = collection["TenDangNhap"];
            var matkhau = collection["MatKhau"];
            var matkhaunhaplai = collection["Matkhaunhaplai"];
            var hoten = collection["HoTenND"];
            var diachi = collection["DiaChi"];
            var email = collection["Email"];
            var dienthoai = collection["DT"];

            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống ";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập ";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu  ";
            }
            else if (matkhaunhaplai != matkhau)
            {
                ViewData["Loi4"] = "Mật khẩu không trùng khớp ";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email không được để trống  ";

            }
            else if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi6"] = "Nhập số điện thoại  ";
            }
            else
            {
                kh.TenDangNhap = tendn;
                kh.MatKhau = matkhau;
                kh.HoTenND = hoten;
                kh.DiaChi = diachi;
                kh.Email = email;
                kh.DT = dienthoai;
                db.NguoiDungs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("Dangnhap");
            }
            return this.DangKy();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        public ActionResult Dangnhap(FormCollection collection, NguoiDung kh)
        {
            var matkhau = collection["Matkhau"];
            var tendn = collection["TenDangNhap"];


            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập ";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu ";
            }

            else
            {
                NguoiDung KH = db.NguoiDungs.SingleOrDefault(n => n.TenDangNhap == tendn && n.MatKhau == matkhau);
                if (KH != null)
                {

                    Session["TenDangNhap"] = KH;
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng ";
            }
            return View();
        }
    }
}