﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaiChi.Core.Mvc.Models;
using TaiChi.Core.Mvc.Extensions;
using Microsoft.Extensions.Logging;
using TaiChi.Core.Interface;
using TaiChi.Core.Mvc.Utility;
using TaiChi.Core.Utility.Filters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaiChi.Core.Mvc.Controllers
{
    /// <summary>
    /// Log4Net日志的使用
    /// 
    /// 
    /// </summary>
    [TypeFilter(typeof(CustomControllerActionFilterAttribute),Order =-1)]
    public class ThirdController : Controller
    {
        private ILoggerFactory _loggerFactory = null;

        private ILogger<SecondController> _logger = null;

        private ITestServiceA _testServiceA = null;

        private ITestServiceB _testServiceB = null;

        private ITestServiceC _testServiceC = null;

        private ITestServiceD _testServiceD = null;

        private IA _a = null;
        public ThirdController(ILoggerFactory loggerFactory,
            ILogger<SecondController> logger,
            ITestServiceA testServiceA,
            ITestServiceB testServiceB,
            ITestServiceC testServiceC,
            ITestServiceD testServiceD,
            IA a)
        {
            _loggerFactory = loggerFactory;
            _logger = logger;
            _testServiceA = testServiceA;
            _testServiceB = testServiceB;
            _testServiceC = testServiceC;
            _testServiceD = testServiceD;
            _a = a;
        }

        // GET: /<controller>/
        [TypeFilter(typeof(CustomActionFilterAttribute),Order =-2)]
        public IActionResult Index(int? id)
        {
            //_testServiceA.Show();
            //_testServiceB.Show();
            //_testServiceC.Show();
            //_testServiceD.Show();
            //_a.Show();
            //var loggerFactory = _loggerFactory.CreateLogger<SecondController>();
            //loggerFactory.LogError("this is SecondController LoggerFactory");
            //_logger.LogError("this is SecondController Logger");
            return View();
        }

    }
}
