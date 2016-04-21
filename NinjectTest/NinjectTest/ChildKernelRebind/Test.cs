using FluentAssertions;
using Ninject;
using Ninject.Activation;
using Ninject.Extensions.ChildKernel;
using Xunit;

namespace NinjectTest.ChildKernelRebind
{
    public class Test
    {
        [Fact]
        public void Foo()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IServiceProvider>().ToMethod(GetServiceProvider);
            kernel.Bind<IService>().To<RootService>();

            var childKernel = new ChildKernel(kernel);
            childKernel.Bind<IService>().To<ChildService>();

            kernel.Get<IServiceProvider>().Provide().Should().BeOfType<RootService>();
            childKernel.Get<IServiceProvider>().Provide().Should().BeOfType<ChildService>();
        }

        private IServiceProvider GetServiceProvider(IContext arg)
        {
            return new ServiceProvider(new RootService());
        }
    }
}