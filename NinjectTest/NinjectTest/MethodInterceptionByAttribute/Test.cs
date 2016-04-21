using Ninject;
using Xunit;

namespace NinjectTest.MethodInterceptionByAttribute
{
    public class Test
    {
        [Fact]
        public void FactMethodName()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IService>().To<Service>();

            kernel.Get<IService>().Intercepted();
            kernel.Get<IService>().NotIntercepted();
        }
    }
}