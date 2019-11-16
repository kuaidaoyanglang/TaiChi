﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaiChi.Core.Utility.Middleware
{
    public class FirstMiddleWare
    {
        private readonly RequestDelegate _next;

        public FirstMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync($"{nameof(FirstMiddleWare)},Hello World1!<br/>");

            await _next(context);

            await context.Response.WriteAsync($"{nameof(FirstMiddleWare)},Hello World2!<br/>");
        }
    }
}
