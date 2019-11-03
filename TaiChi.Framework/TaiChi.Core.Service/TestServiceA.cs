using TaiChi.Core.Interface;
using System;

namespace TaiChi.Core.Service
{
    public class TestServiceA : ITestServiceA
    {
        public void Show()
        {
            Console.WriteLine("A123456");
        }
    }
}
