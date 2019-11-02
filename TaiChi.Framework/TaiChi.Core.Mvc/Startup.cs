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
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSession();
        }

        /// <summary>
        /// ʹ��ע������ķ���Ҳ�Ǳ����л��������á�
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory loggerFactory)
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
