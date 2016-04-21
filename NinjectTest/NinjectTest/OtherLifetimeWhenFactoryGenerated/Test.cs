using FluentAssertions;
using Ninject;
using Ninject.Activation;
using Ninject.Extensions.Factory;
using System;
using Xunit;

namespace NinjectTest.OtherLifetimeWhenFactoryGenerated
{
    public class Test
    {
        [Fact]
        public void DoTest()
        {
            var kernel = new StandardKernel();
            kernel.Bind<ToBeCreated>().ToSelf().InSingletonScope();

            kernel.Bind<ToBeCreated>().ToSelf().When(CheckCondition);

            kernel.Bind<IFactory>().ToFactory();

            Guid id1 = kernel.Get<ToBeCreated>().Id;
            Guid id2 = kernel.Get<ToBeCreated>().Id;

           
            id1.Should().NotBe(id2);
        }

        private bool CheckCondition(IRequest arg)
        {
            return false;
        }
    }
}