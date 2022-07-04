using Microsoft.AspNetCore.Mvc;
using MSIT141Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSIT141Site.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index(User user)
        {
            //System.Threading.Thread.Sleep(5000); //停止5秒鐘
            if (string.IsNullOrEmpty(user.name))
                user.name = "Ajax";
            return Content($"Hello {user.name}你好，年紀是{user.age}，電子郵件是{user.email}!!", "text/plain", System.Text.Encoding.UTF8);
        }
    }
}
