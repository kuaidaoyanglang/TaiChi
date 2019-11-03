using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaiChi.Core.Mvc.Utility
{
    public class CustomAutofacAOP : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"The method is:{invocation.Method.Name}");
            Console.WriteLine($"The aruments is:{string.Join(',',invocation.Arguments)}");
            invocation.Proceed();
            Console.WriteLine("My name is davy");
        }
    }

    public interface IA 
    {
        void Show();
    }
    [Intercept(typeof(CustomAutofacAOP))]
    public class A : IA
    {
        public void Show()
        {
            Console.WriteLine("this is davy");
        }
    }
}
