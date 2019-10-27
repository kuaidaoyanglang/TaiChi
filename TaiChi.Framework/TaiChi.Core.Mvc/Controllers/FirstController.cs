using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaiChi.Core.Mvc.Models;
using TaiChi.Core.Mvc.Extensions;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaiChi.Core.Mvc.Controllers
{
    public class FirstController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index(int? id)
        {
            base.ViewData["User1"] = new CurrentUser()
            {
                Id = 7,
                Name = "Y",
                Account = " ╰つ Ｈ ♥. 花心胡萝卜",
                Email = "莲花未开时",
                Password = "落单的候鸟",
                LoginTime = DateTime.Now
            };

            base.ViewData["Something"] = 12345;

            base.ViewBag.Name = "Richard";
            base.ViewBag.Description = "Teacher";
            base.ViewBag.User = new CurrentUser()
            {
                Id = 7,
                Name = "IOC",
                Account = "限量版",
                Email = "莲花未开时",
                Password = "落单的候鸟",
                LoginTime = DateTime.Now
            };

            //base.TempData["User"] = new CurrentUser()
            //{
            //    Id = 7,
            //    Name = "CSS",
            //    Account = "季雨林",
            //    Email = "KOKE",
            //    Password = "落单的候鸟",
            //    LoginTime = DateTime.Now
            //};//后台可以跨action  基于session

            base.TempData.Put("User", new CurrentUser()
            {
                Id = 7,
                Name = "CSS",
                Account = "季雨林",
                Email = "KOKE",
                Password = "落单的候鸟",
                LoginTime = DateTime.Now
            });

            if (id == null)
            {
                //return this.Redirect("~/First/TempDataPage");//未完待续
                //return this.re
                return this.Redirect("~/First/TempDataPage"); // 作为一个待处理项
            }

            else
                return View(new CurrentUser()
                {
                    Id = 7,
                    Name = "一点半",
                    Account = "季雨林",
                    Email = "KOKE",
                    Password = "落单的候鸟",
                    LoginTime = DateTime.Now
                });
        }

        public IActionResult TempDataPage()
        {
            base.ViewBag.User = base.TempData.Get<CurrentUser>("User");//可以拿到数据
            return View();
        }
    }
}
