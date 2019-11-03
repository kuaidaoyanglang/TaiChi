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
        ///  IServiceCollection : ��ʵ��һ������ 
        ///  ������ʹ�ã�
        ///            ʵ����һ��������
        ///            ע��
        ///            ��ȡ����
        ///  ����Autofac
        /// 1������autofac Autofac.Extensions.DependencyInjection
        /// 2��ConfigureServices��Ҫ������ֵ IServiceProvider
        /// 3��ʵ��������
        /// 4��ע�����
        /// 5������AutofacServiceProvider ��ʵ��
        /// 
        /// Autofac֧��Aop
        /// 
        /// ��Framework�����£�Ȩ������  Action/Result Exception
        /// 
        ///         ��Ϊ�����������֮�����
        /// 
        /// Core�� ����ResourceFilter Action/Result Exception
        ///         Action/Result Exception��������û��ʲô�仯��
        ///         
        ///�ֱ��ȫ�֡���������action ע���� ActionFiler ִ��˳��  ȫ��OOnActionExecuting ��������OnActionExecuting  Action OnActionExecutingd  Action  Action OnActionExecuted �������� OnActionExecuted  ȫ�ֵ�OnActionExecuted  ������һ������˹����
        ///
        /// Order ����������ִ��˳���մ�С�����˳��ִ�У�
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
