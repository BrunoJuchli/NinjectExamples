using System;
using System.Collections;
using System.Threading;
using FluentAssertions;
using Ninject;
using Xunit;

namespace NinjectTest.MasstransitSubscription
{
    public class MultiConstantBinding
    {
        [Fact]
        public void Test()
        {
            var kernel = new StandardKernel();

            kernel.Bind<FakeParamterizedType>().ToConstant(new FakeParamterizedType(1));
            kernel.Bind<FakeParamterizedType>().ToConstant(new FakeParamterizedType(2));
            kernel.Bind<FakeParamterizedType>().ToConstant(new FakeParamterizedType(3));
            kernel.Bind<FakeParamterizedType>().ToConstant(new FakeParamterizedType(4));

            kernel.GetAll<FakeParamterizedType>().Should().HaveCount(4);
        }

        [Fact]
        public void TestThatTypeImplementInterface()
        {
            Type interf = typeof(IDisposable);
            Type t = typeof(ManualResetEventSlim);

            interf.IsAssignableFrom(t).Should().BeTrue();
        }
    }

    public class FakeParamterizedType
    {
        public int Parameter { get; set; }

        public FakeParamterizedType(int parameter)
        {
            Parameter = parameter;
        }
    }
}