using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Syntax;
using Ninject.Activation;
using Ninject.Planning.Targets;
using Xunit;
using FluentAssertions;

namespace NinjectTest.SO45218640
{
    interface IService { }

    class ServiceA : IService { }
    class ServiceB : IService { }
    class ServiceDefault : IService { }

    class ServiceUse
    {
        public ServiceUse(IService svc) { }
    }

    static class ServiceNames
    {
        public const string ServiceA = "ServiceA";
        public const string ServiceB = "ServiceB";
        public const string ServiceDefault = "ServiceDefault";
    }

    class ServiceProvider : Provider<IService>
    {
        protected override IService CreateInstance(IContext context)
        {
            string bindingName = "ServiceA";

            if (context.Kernel.CanResolve<IService>(bindingName))
                return context.Kernel.Get<IService>(bindingName);

            return context.Kernel.Get<IService>(ServiceNames.ServiceDefault);
        }
    }

    public class IntegrationTest
    {
        [Fact]
        public void Test()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IService>().To<ServiceA>()
                .Named(ServiceNames.ServiceA);
            kernel.Bind<IService>().To<ServiceB>()
                .Named(ServiceNames.ServiceB);
            kernel.Bind<IService>().To<ServiceDefault>()
                .Named(ServiceNames.ServiceDefault);
            kernel.Bind<IService>().ToProvider<ServiceProvider>()
                .When(request => true);

            kernel.Get<ServiceUse>().Should().BeOfType<ServiceUse>();
        }
    }

    public static class NinjectExtensions
    {
        public static IBindingInNamedWithOrOnSyntax<T> MakePreferredBinding<T>(this IBindingWhenSyntax<T> syntax)
        {
            return syntax.When(req => true);
        }
    }
}
