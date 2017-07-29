using FluentAssertions;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NinjectTest.SO39610282
{
    public class Test
    {
        [Fact]
        public void TestMethod()
        {
            var kernel = new StandardKernel();

            kernel.GetAll<IFoo>().Should().BeEmpty();
        }

        [Fact]
        public void CtorInjection()
        {

        }
    }

    public interface IFoo
    {
    }

    public class Foo { }
}
