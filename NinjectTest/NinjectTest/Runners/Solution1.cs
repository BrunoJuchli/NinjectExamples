using Ninject;
using Ninject.Extensions.Factory;
using Xunit;

namespace NinjectTest.Runners
{
    public class Solution1
    {
        [Fact]
        public void Execute()
        {
            var kernel = new StandardKernel();

            // you could also use .InNamedScope() or maybe InParentScope() or InCallScope() -- see the NamedScope extension!
            kernel.Bind<IControl>().To<Control>().InSingletonScope();

            // implementation is auto generated.
            kernel.Bind<IRunnerFactory>().ToFactory();

        }
    }
}