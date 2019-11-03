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
        /// asp.net �д�globle��ִ��
        ///     ������IIS�ϣ�IIS�����˿ڣ���ת������.net framewrokվ����ҵ���߼�������Ӧ����
        /// 
        /// core ʵ������һ������̨����,�����main���롣
        /// 
        /// 
        /// ����
        ///     ȷ������ʱ��ȷ��װ��
        ///     ѡ�񷢲���Ŀ��������ָ���ļ��У�
        ///     �½�վ�㣻
        ///     �������ѡ����йܴ���
        ///     
        /// appsetting.json��Ӧ��Web.config;Ĭ�ϲ���json��ʽ�洢��
        /// asp.net:ConfigurationManager
        /// asp.net core:������IConfiguration
        /// 
        /// 
        /// Log4Net ���ɵ�Core
        ///     ����log4net
        ///     Microsoft.Extensions.Logging.Log4Net.AspNetCore
        ///     ���Log4Net�����ļ�
        ///     ע��ILoggerFactory
        ///     ����Ilogger����
        ///     д��־
        ///     
        /// IServiceCollection:��ʵ��һ������
        ///     
        /// 
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)  //����һ��WebHost,�������ǿ�ƽ̨��
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context,ILoggingBuilder)=> {
                    ILoggingBuilder.AddFilter("System",LogLevel.Warning);// ����ϵͳ��������־
                    ILoggingBuilder.AddFilter("Microsoft",LogLevel.Warning);//����ϵͳ��������־
                    ILoggingBuilder.AddLog4Net();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
