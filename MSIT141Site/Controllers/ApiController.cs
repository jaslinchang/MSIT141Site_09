using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSIT141Site.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MSIT141Site.Controllers
{
    public class ApiController : Controller
    {
        private readonly DemoContext _context;
        private readonly IWebHostEnvironment _host;

        public ApiController(DemoContext conetxt, IWebHostEnvironment hostEnvironment)
        {
            _context = conetxt;
            _host = hostEnvironment;
        }

        public IActionResult Index(User user)
        {
            //System.Threading.Thread.Sleep(5000); //停止5秒鐘
            if (string.IsNullOrEmpty(user.name))
                user.name = "Ajax";
            return Content($"Hello {user.name}你好，年紀是{user.age}，電子郵件是{user.email}!!", "text/plain", System.Text.Encoding.UTF8);
        }

        public IActionResult Register(Member member, IFormFile file)
        {
            //檔案上傳要有實際路徑
            //C:\Users\Student\Documents\Ajax\MSIT141Site\wwwroot\uploads
            //string path = _host.ContentRootPath; //會取得專案資料夾的實際路徑
            string path = Path.Combine(_host.WebRootPath, "uploads", file.FileName); 
            //會取得專案資料夾下wwwroot的實際路徑，並用Path.Combine將我的upload資料夾路徑合併
            using (var fileStream = new FileStream(path, FileMode.Create))  //用using是為了用完要關掉
            {//透過資料流把檔案存到這個路徑
                file.CopyTo(fileStream); //儲存檔案到uploads資料夾中
            }
            //===================================
            //寫進資料庫
            byte[] imgByte = null;
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                imgByte = memoryStream.ToArray();
            }
            member.FileName = file.FileName;
            member.FileData = imgByte;

            _context.Members.Add(member);
            _context.SaveChanges();

            string info = $"{file.FileName} - {file.ContentType} - {file.Length}";
            return Content(info, "text/plain", System.Text.Encoding.UTF8);
        }

        public IActionResult CheckAccount(string name)
        {
            var exists = _context.Members.Any(m => m.Name == name);
            return Content(exists.ToString(), "text/plain");
        }

        public IActionResult GetImageBytes(int id = 1)
        {
            Member member = _context.Members.Find(id);
            byte[] img = member.FileData;
            return File(img, "image/jpeg");
        }

        //讀取所有城市的資料
        public IActionResult City()
        {
            var cities = _context.Addresses.Select(a => a.City).Distinct();
            return Json(cities);
        }

        //根據城市名稱讀出鄉鎮區的資料
        public IActionResult Districts(string city)
        {
            var districts = _context.Addresses.Where(a => a.City == city).Select(a => a.SiteId).Distinct();
            return Json(districts);
        }

        //根據鄉鎮區的資料讀出路名
        public IActionResult Roads(string district)
        {
            var roads = _context.Addresses.Where(a => a.SiteId == district).Select(a => a.Road);
            return Json(roads);
        }
        public IActionResult Members()
        {
            return Json(_context.Members);
        }

    }
}
