using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        /// �����ļ���ȡ�����캯��ע��
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// ע�����������ã�������ʱ���캯�����ã�ʹ�õ�ǰ������������ӵ��������档��̬����Ҳ�Ǳ����캯�����á�
        /// 
        /// 1������Autofac��Autofac.Extensions.DependencyInjection��
        /// 2��ConfigureServices��Ҫ����ֵ,IServiceProvider��
        /// 3��ʵ����������
        /// 4��ע�����
        /// 5������AutofacServiceProvider��
        /// 
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options=> 
            { 
                //ע��ȫ���쳣�������
                options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                options.Filters.Add(typeof(CustomGlobalActionFilterAttribute));
            }).AddControllersAsServices();
            services.AddSession();

            

            //services.AddScoped<CustomActionFilterAttribute>();
            //����һ������
            //ContainerBuilder containerBuilder = new ContainerBuilder();

            //serviceĬ�ϵ�����»�ע����񣬻�Ҫʵ������������صĴ���
            //containerBuilder.Populate(services);
            //ע�����ʵ��
            //containerBuilder.RegisterModule<CustomAutofacModule>();

            //builderһ������
            //IContainer container = containerBuilder.Build();
            //return new AutofacServiceProvider(container);

            //services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            // ���߽�Controller���뵽Services�У�����д����Ĵ���Ϳ���ʡ����
            //services.AddControllersWithViews().AddControllersAsServices();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<CustomAutofacModule>();

            #region �����������ļ����÷��� 
            // ʵ����
            IConfigurationBuilder config = new ConfigurationBuilder();
            //ָ�������ļ�  �����Ĭ�������ļ���·���ڸ�Ŀ¼�£��θ���ʵ���������
            config.AddJsonFile("autofac.json");
            // Register the ConfigurationModule with Autofac. 
            IConfigurationRoot configBuild = config.Build();
            //��ȡ�����ļ���������Ҫע��ķ���
            var module = new ConfigurationModule(configBuild);
            builder.RegisterModule(module);
            // �������ݷ���Home/Index ��
            #endregion

            #region ע�����  �������ļ�ע�����
            //// ע�����
            //containerbuilder.RegisterType<TestServiceA>().As<ITestServiceA>().SingleInstance();
            //containerbuilder.RegisterType<TestServiceB>().As<ITestServiceB>().SingleInstance();
            //containerbuilder.RegisterType<TestServiceC>().As<ITestServiceC>().SingleInstance(); 
            /////��������ӿڵ�ʵ��  ����ȫ��ע�ᵽ��������
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
        /// ע�����������ã�������ʱ���캯�����ã�ʹ�õ�ǰ������������ӵ��������档��̬����Ҳ�Ǳ����캯�����á�
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSession();
        }*/

        /// <summary>
        /// ʹ��ע������ķ���Ҳ�Ǳ����л��������á�
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            //framework ȫ����װ�õ�25���¼�������ͨ��ע���¼������ҵ���߼�

            //Core�����˳�����Լ�����ġ�ȫ����Ҫ�Լ�ƴװ��

            //string helloWorld = "Hello World!";
            //byte[] vs = System.Text.Encoding.Default.GetBytes(helloWorld);
            //app.Run(a => a.Response.Body.WriteAsync(vs,0,vs.Length));//�ж�

            var _logger = loggerFactory.CreateLogger<Startup>();

            _logger.LogError("this is a Startup class Error");

            #region Asp.Net Core��ȡ�����ļ���JSON�ļ��� 
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

            // ���ǻ��� IApplicationBuilder

            //AuthenticationHttpContextExtensions

            /*   app.Run(c => c.Response.WriteAsync("Hello World!")); // �ս�*/


            //1 Run �ս�ʽ  ֻ��ִ�У�û��ȥ����Next  
            //һ����Ϊ�ս��
            //app.Run(async (HttpContext context) =>
            //{
            //    await context.Response.WriteAsync("Hello World Run");  // û��next  û��ȥִ����һ���м��
            //});

            //app.Run(async (HttpContext context) =>
            //{
            //    await context.Response.WriteAsync("Hello World Run Again");
            //});
            //app.Use(next =>   // next ��һ������ֵ  ��Ϊ��һ���м����һ������
            //{
            //    Console.WriteLine("this is Middleware1");
            //    return new RequestDelegate(async context =>
            //    {
            //        await context.Response.WriteAsync("<h3>This is Middleware1 start</h3>");
            //        await next.Invoke(context);
            //        await context.Response.WriteAsync("<h3>This is Middleware1 end</h3>");
            //    });
            //});

            //app.Use(next =>
            //{
            //    Console.WriteLine("this is Middleware2");
            //    return new RequestDelegate(async context =>
            //    {
            //        await context.Response.WriteAsync("<h3>This is Middleware2 start</h3>");
            //        await next.Invoke(context);
            //        await context.Response.WriteAsync("<h3>This is Middleware2 end</h3>");
            //    });
            //});

            //app.Use(next =>
            //{
            //    Console.WriteLine("this is Middleware2");
            //    return new RequestDelegate(async context =>
            //    {
            //        await context.Response.WriteAsync("<h3>This is Middleware3 start</h3>");
            //        await next.Invoke(context);
            //        await context.Response.WriteAsync("<h3>This is Middleware3 end</h3>");
            //    });
            //});

            ////2 Use��ʾע�ᶯ�� �����ս��
            ////ִ��next���Ϳ���ִ����һ���м�� �����ִ�У��͵���Run
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello World Use1 <br/>");
            //    await next();
            //    await context.Response.WriteAsync("Hello World Use1 End <br/>");
            //});
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello World Use2 Again <br/>");
            //    await next();
            //});

            //UseWhen���Զ�HttpContext�������Ӵ�����;ԭ�������̻�������ִ�е�
            //app.UseWhen(context =>
            //{
            //    return context.Request.Query.ContainsKey("Name");
            //},
            //appBuilder =>
            //{
            //    appBuilder.Use(async (context, next) =>
            //    {
            //        await context.Response.WriteAsync("Hello World Use3 Again Again Again <br/>");
            //        await next();
            //    });
            //});


            //IEnumerable<ITestServiceD> testServiceDs = IEnumerable <ITestServiceD> testServiceDs =

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                //HSTS��Ĭ��ֵΪ30�졣 ������Ҫ��������������Ĵ����ã���μ�https://aka.ms/aspnetcore-hsts��
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
     �� ASP.NET Core 3.0 ��Ҫ������������·�ɣ����� app.UseRouting �� app.UseEndpoints ��������ʲô��
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
     ������������app.UseRouting������app.UseEndpoints�������á� ǰ�߽�ע�������߼���ȷ��·�ɵ��м���� ���߽�ִ�и�·�ɡ�

     
    */
}
