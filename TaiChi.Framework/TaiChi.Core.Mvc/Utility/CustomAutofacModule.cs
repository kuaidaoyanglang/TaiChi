using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaiChi.Core.Interface;
using TaiChi.Core.Service;

namespace TaiChi.Core.Mvc.Utility
{
    public class CustomAutofacModule:Module
    {
        /// <summary>
        /// 当前这个Module专做服务注册
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TestServiceA>().As<ITestServiceA>().SingleInstance();
            builder.RegisterType<TestServiceB>().As<ITestServiceB>();
            builder.RegisterType<TestServiceC>().As<ITestServiceC>();
            builder.RegisterType<TestServiceD>().As<ITestServiceD>();

            base.Load(builder);
        }
    }
}
