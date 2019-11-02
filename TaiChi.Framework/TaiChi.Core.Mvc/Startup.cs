using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TaiChi.Core.Mvc
{
    public class Startup
    {
        /// <summary>
        /// 配置文件读取，构造函数注入
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 注册服务：零个引用，被运行时构造函数调用，使用当前方法将服务添加到容器里面。静态函数也是被构造函数调用。
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSession();
        }

        /// <summary>
        /// 使用注册进来的服务。也是被运行环境所调用。
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory loggerFactory)
        {

            //framework 全部封装好的25个事件，可以通过注册事件，添加业务逻辑

            //Core里面的顺序是自己定义的。全部需要自己拼装。

            //string helloWorld = "Hello World!";
            //byte[] vs = System.Text.Encoding.Default.GetBytes(helloWorld);
            //app.Run(a => a.Response.Body.WriteAsync(vs,0,vs.Length));//中断

            var _logger = loggerFactory.CreateLogger<Startup>();

            _logger.LogError("this is a Startup class Error");

            #region Asp.Net Core读取配置文件（JSON文件） 
            //xml path
            Console.WriteLine($"option1 = {this.Configuration["Option1"]}");
            Console.WriteLine($"option2 = {this.Configuration["option2"]}");
            Console.WriteLine(
                $"suboption1 = {this.Configuration["subsection:suboption1"]}");
            Console.WriteLine("Wizards:");
            Console.Write($"{this.Configuration["wizards:0:Name"]}, ");
            Console.WriteLine($"age {this.Configuration["wizards:0:Age"]}");
            Console.Write($"{this.Configuration["wizards:1:Name"]}, ");
            Console.WriteLine($"age {this.Configuration["wizards:1:Age"]}");
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                //HSTS的默认值为30天。 您可能要针对生产方案更改此设置，请参见https://aka.ms/aspnetcore-hsts。
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    /*
     在 ASP.NET Core 3.0 需要下面这样配置路由，请问 app.UseRouting 与 app.UseEndpoints 的区别是什么？
     app.UseRouting();
     app.UseEndpoints(endpoints =>
     {
         endpoints.MapControllerRoute(
             name: "default",
             pattern: "{controller=Home}/{action=Index}/{id?}");
         endpoints.MapRazorPages();
     });

     https://stackoverflow.com/questions/56156657/no-overload-for-method-userouting-takes-1-arguments
     https://docs.microsoft.com/en-us/aspnet/core/migration/22-to-30?view=aspnetcore-2.2&tabs=visual-studio#update-routing-startup-code

     The two steps are set up by app.UseRouting() and app.UseEndpoints(). The former will register the middleware that runs the logic to determine the route. The latter will then execute that route.
     这两个步骤由app.UseRouting（）和app.UseEndpoints（）设置。 前者将注册运行逻辑以确定路由的中间件。 后者将执行该路由。

     
    */
}
