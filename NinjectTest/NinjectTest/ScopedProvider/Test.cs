using Ninject;
using Xunit;

namespace NinjectTest.ScopedProvider
{
    public class Test
    {
        [Fact]
        public void FactMethodName()
        {
            var kernel = new StandardKernel();

            kernel.Bind<string>().ToProvider(new StringProvider()).InSingletonScope();

            kernel.Get<string>();
            kernel.Get<string>();
            kernel.Get<string>();
        }
    }
}