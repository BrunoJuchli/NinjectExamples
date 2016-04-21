using Ninject.Extensions.Interception;
using System;

namespace NinjectTest.MethodInterceptionByAttribute
{
    public class LogInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("before");

            invocation.Proceed();

            Console.WriteLine("after");
        }
    }
}