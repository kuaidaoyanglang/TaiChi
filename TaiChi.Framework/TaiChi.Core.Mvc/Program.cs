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
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
