﻿using Microsoft.AspNetCore.Mvc;
using MSIT141Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSIT141Site.Controllers
{
    public class ApiController : Controller
    {
        private readonly DemoContext _context;

        public ApiController(DemoContext conetxt)
        {
            _context = conetxt;
        }

        public IActionResult Index(User user)
        {
            //System.Threading.Thread.Sleep(5000); //停止5秒鐘
            if (string.IsNullOrEmpty(user.name))
                user.name = "Ajax";
            return Content($"Hello {user.name}你好，年紀是{user.age}，電子郵件是{user.email}!!", "text/plain", System.Text.Encoding.UTF8);
        }
        public IActionResult CheckName(Member user)
        {
            var datas = _context.Members.FirstOrDefault(m => m.Name == user.Name);
            string message = "";
            if (datas != null)
            {
                message = "帳號已存在";
            }
            else
                message = "此帳號可以使用";

            return Content(message);

        }
    }
}
