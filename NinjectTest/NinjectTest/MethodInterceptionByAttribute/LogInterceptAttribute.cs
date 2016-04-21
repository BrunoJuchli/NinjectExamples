using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Attributes;
using Ninject.Extensions.Interception.Request;

namespace NinjectTest.MethodInterceptionByAttribute
{
    public class LogInterceptAttribute : InterceptAttribute
    {
        public override IInterceptor CreateInterceptor(IProxyRequest request)
        {
            return new LogInterceptor();
        }
    }
}