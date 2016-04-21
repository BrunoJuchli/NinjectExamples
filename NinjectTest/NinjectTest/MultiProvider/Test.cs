using FluentAssertions;
using Ninject;
using System.Collections.Generic;
using Xunit;

namespace NinjectTest.MultiProvider
{
    public class Test
    {
        [Fact]
        public void KernelGet()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IEnumerable<string>>().ToProvider<EnumerableStringProvider>();

            var strings = kernel.Get<IEnumerable<string>>();

            strings.Should().HaveCount(2);
        }

        [Fact]
        public void CtorInjection()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IReadOnlyCollection<string>>().ToProvider<EnumerableStringProvider>();

            var strings = kernel.Get<EnumerableStringConsumer>().Strings;

            strings.Should().HaveCount(2);
        }
    }
}