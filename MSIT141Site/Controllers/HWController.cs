using Microsoft.AspNetCore.Mvc;
using MSIT141Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSIT141Site.Controllers
{
    public class HWController : Controller
    {
        private readonly DemoContext _context;

        public HWController(DemoContext conetxt)
        {
            _context = conetxt;
        }
        public IActionResult HW01()
        {
            return View();
        }
        public IActionResult HW02_Register()
        {   //註冊會員=>帳號是否存在檢查
            //製作會員註冊頁面
            //使用者輸入姓名後，透過Ajax技術將姓名送到伺服器端
            //伺服器端收到後，到資料庫檢查資料是否存在，將結果回傳到Client端
            //Client端根據收到的結果顯示帳號是否存在的訊息
            return View();
        }
        //public IActionResult CheckName(Member user)
        //{
        //    var datas = _context.Members.FirstOrDefault(m => m.Name == user.Name);
        //    string message = "";
        //    if (datas != null)
        //    {
        //        message = "帳號已存在";
        //    }
        //    else
        //        message = "此帳號可以使用";

        //    return Content(message);

        //}
        public IActionResult CheckAccount(string name)
        {
            var exists = _context.Members.Any(m => m.Name == name);
            return Content(exists.ToString(), "text/plain");
        }
    }
}
