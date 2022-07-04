using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSIT141Site.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index(string name,int age=0)
        {
            if (string.IsNullOrEmpty(name))
                name = "Ajax";
            return Content($"Hello {name}你好，年紀是{age}!!", "text/plain", System.Text.Encoding.UTF8);
        }
    }
}
