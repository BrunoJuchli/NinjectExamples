using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Ninject;
using Xunit;

namespace NinjectTest.MultiConfiguredBinding
{
    public class Test
    {
        [Fact]
        public void FactMethodName()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IBar>().To<Bar>();
            kernel.Bind<IFoo>().To<Foo1>();
            kernel.Bind<IFoo>().To<Foo2>().WithConstructorArgument("theParametersName", true);
            kernel.Bind<IFoo>().To<Foo2>().WithConstructorArgument("theParametersName", false);

            List<IFoo> foos = kernel.GetAll<IFoo>().ToList();

            foos.Should().HaveCount(3);
        } 
    }
}