using Ninject;
using Ninject.Activation;
using Xunit;

namespace NinjectTest
{
    public class Bar { }

    public class Foo
    {
        public Foo(Bar bar)
        {
        }
    }

    public class ProviderTest
    {
        [Fact]
        public void FactMethodName()
        {
            var kernel = new StandardKernel();

            kernel.Bind<Bar>().ToProvider<BarProvider>();

            kernel.Get<Foo>();
        }
    }

    public class BarProvider : Provider<Bar>
    {
        protected override Bar CreateInstance(IContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}