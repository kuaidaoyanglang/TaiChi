using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaiChi.Core.Interface;
using TaiChi.Core.Mvc.Utility;
using TaiChi.Core.Service;
using TaiChi.Core.Utility.Filters;

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
        /// 
        /// 1、引入Autofac、Autofac.Extensions.DependencyInjection；
        /// 2、ConfigureServices需要返回值,IServiceProvider；
        /// 3、实例化容器；
        /// 4、注册服务；
        /// 5、返回AutofacServiceProvider；
        /// 
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options=> 
            { 
                //注册全局异常处理对象
                options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                options.Filters.Add(typeof(CustomGlobalActionFilterAttribute));
            }).AddControllersAsServices();
            services.AddSession();

            

            //services.AddScoped<CustomActionFilterAttribute>();
            //申明一个容器
            //ContainerBuilder containerBuilder = new ContainerBuilder();

            //service默认的情况下会注册服务，还要实例化控制器相关的代码
            //containerBuilder.Populate(services);
            //注册服务实例
            //containerBuilder.RegisterModule<CustomAutofacModule>();

            //builder一个容器
            //IContainer container = containerBuilder.Build();
            //return new AutofacServiceProvider(container);

            //services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            // 或者将Controller加入到Services中，这样写上面的代码就可以省略了
            //services.AddControllersWithViews().AddControllersAsServices();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<CustomAutofacModule>();

            #region 依赖于配置文件配置服务 
            // 实例化
            IConfigurationBuilder config = new ConfigurationBuilder();
            //指定配置文件  这里的默认配置文件的路径在根目录下，课根据实际情况调整
            config.AddJsonFile("autofac.json");
            // Register the ConfigurationModule with Autofac. 
            IConfigurationRoot configBuild = config.Build();
            //读取配置文件里配置需要注册的服务
            var module = new ConfigurationModule(configBuild);
            builder.RegisterModule(module);
            // 测试内容放在Home/Index 下
            #endregion

            #region 注册服务  非配置文件注册服务
            //// 注册服务
            //containerbuilder.RegisterType<TestServiceA>().As<ITestServiceA>().SingleInstance();
            //containerbuilder.RegisterType<TestServiceB>().As<ITestServiceB>().SingleInstance();
            //containerbuilder.RegisterType<TestServiceC>().As<ITestServiceC>().SingleInstance(); 
            /////添加两个接口的实现  这里全部注册到容器中来
            //builder.RegisterType<TestServiceD>().As<ITestServiceD>().SingleInstance();
            //builder.RegisterType<TestServiceD_Test>().As<ITestServiceD>().SingleInstance();
            //IContainer container = builder.Build();

            //IEnumerable<ITestServiceD> testServiceDs = container.Resolve<IEnumerable<ITestServiceD>>();

            //foreach (var item in testServiceDs)
            //{
            //    item.Show();
            //}
            //containerbuilder.RegisterModule<CustomAutofacModule>();   
            #endregion
        }

        /*
        /// <summary>
        /// 注册服务：零个引用，被运行时构造函数调用，使用当前方法将服务添加到容器里面。静态函数也是被构造函数调用。
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSession();
        }*/

        /// <summary>
        /// 使用注册进来的服务。也是被运行环境所调用。
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
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

            //IEnumerable<ITestServiceD> testServiceDs = IEnumerable <ITestServiceD> testServiceDs =

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
                //endpoints.MapRazorPages();
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
