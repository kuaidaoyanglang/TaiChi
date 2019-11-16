using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaiChi.Core.Utility.Filters;

namespace TaiChi.Core.Mvc.Controllers
{
    public class TestController : Controller
    {
        [CustomResourceFilter]
        public IActionResult Index()
        {
            ViewBag.Now = DateTime.Now;
            return View();
        }
    }
}