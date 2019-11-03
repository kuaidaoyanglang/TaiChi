using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TaiChi.Core.Mvc
{
    public class Program
    {
        /// <summary>
        /// asp.net 中从globle中执行
        ///     寄宿在IIS上，IIS监听端口，做转发，有.net framewrok站点做业务逻辑处理，响应请求。
        /// 
        /// core 实际上是一个控制台程序,程序从main进入。
        /// 
        /// 
        /// 部署：
        ///     确保运行时正确安装；
        ///     选择发布项目，发布到指定文件夹；
        ///     新建站点；
        ///     程序池里选择非托管代吗；
        ///     
        /// appsetting.json对应于Web.config;默认采用json格式存储；
        /// asp.net:ConfigurationManager
        /// asp.net core:依赖于IConfiguration
        /// 
        /// 
        /// Log4Net 集成到Core
        ///     引入log4net
        ///     Microsoft.Extensions.Logging.Log4Net.AspNetCore
        ///     添加Log4Net配置文件
        ///     注入ILoggerFactory
        ///     创建Ilogger对象
        ///     写日志
        ///     
        ///  IServiceCollection : 其实是一个容器 
        ///  容器的使用：
        ///            实例化一个容器；
        ///            注册
        ///            获取服务
        ///  整合Autofac
        /// 1、引入autofac Autofac.Extensions.DependencyInjection
        /// 2、ConfigureServices需要返返回值 IServiceProvider
        /// 3、实例化容器
        /// 4、注册服务
        /// 5、返回AutofacServiceProvider 的实例
        /// 
        /// Autofac支持Aop
        /// 
        /// 在Framework环境下：权限特性  Action/Result Exception
        /// 
        ///         因为特性是随编译之后存在
        /// 
        /// Core： 加了ResourceFilter Action/Result Exception
        ///         Action/Result Exception三个特性没有什么变化。
        ///         
        ///分别对全局、控制器、action 注册了 ActionFiler 执行顺序：  全局OOnActionExecuting 控制器的OnActionExecuting  Action OnActionExecutingd  Action  Action OnActionExecuted 控制器的 OnActionExecuted  全局的OnActionExecuted  类似于一个俄罗斯套娃
        ///
        /// Order 是用做排序，执行顺序按照从小到大的顺序执行；
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)  //创建一个WebHost,服务器是跨平台的
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context,ILoggingBuilder)=> {
                    ILoggingBuilder.AddFilter("System",LogLevel.Warning);// 忽略系统的其他日志
                    ILoggingBuilder.AddFilter("Microsoft",LogLevel.Warning);//忽略系统的其他日志
                    ILoggingBuilder.AddLog4Net();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
