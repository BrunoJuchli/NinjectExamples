using System;

namespace NinjectTest.MethodInterceptionByAttribute
{
    internal class Service : IService
    {
        [LogInterceptAttribute]
        public virtual void Intercepted()
        {
            Console.WriteLine("Intercepted() method");
        }

        public virtual void NotIntercepted()
        {
            Console.WriteLine("NotIntercepted() method");
        }
    }
}