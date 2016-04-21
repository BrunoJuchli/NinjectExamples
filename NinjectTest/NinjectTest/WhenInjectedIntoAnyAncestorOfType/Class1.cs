using System;
using Ninject;
using Ninject.Activation;
using Xunit;

namespace NinjectTest.WhenInjectedIntoAnyAncestorOfType
{
    public class Class1
    {
        [Fact]
        public void Test()
        {
            var kernel = new StandardKernel();

            var binding = kernel.Bind<string>().ToSelf();
            Func<IRequest, bool> whenInjectedIntoCondition = binding.WhenInjectedInto<int>().BindingConfiguration.Condition;
            binding.WhenAnyAncestorMatches(c => whenInjectedIntoCondition(c.Request));
        }
    }
}