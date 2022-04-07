using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TruyenKhanh.Models
{
    public class ListTruyen
    {
        dbFSDataContext db = new dbFSDataContext();
        public int MaTruyen { get; set; }
        [Display(Name = "Ten Truyen ")]
        public string TenTruyen { get; set; }

        [Display(Name = "Mo Ta")]
        public string MoTa { get; set; }

        [Display(Name = "Hinh  ")]
        public string Hinh { get; set; }
        [Display(Name = "Noi Dung")]
        public string NoiDung { get; set; }

        [Display(Name = "Ngay XB ")]
        public DateTime NgayXB { get; set; }
        [Display(Name = "So Chuong ")]
        public int SoChuong { get; set; }

        [Display(Name = "Ma TG ")]
        public int MaTG { get; set; }
    }
}