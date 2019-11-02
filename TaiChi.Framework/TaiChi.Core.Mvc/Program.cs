using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                });
    }
}
