using FluentAssertions;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NinjectTest.MultiInjectionWithWhen
{
    public class ConfigLoader
    {
        public ConfigLoader(IEnumerable<string> commands)
        {
            Commands = commands.ToList();
        }

        public IReadOnlyCollection<string> Commands { get; private set; }
    }

    public class Test
    {


        [Fact]
        public void Foo()
        {
            const string expected1 = "Any";
            const string expected2 = "Other";

            var kernel = new StandardKernel();
            kernel.Bind<string>().ToConstant(expected1)
                .WhenInjectedExactlyInto<ConfigLoader>();
            kernel.Bind<string>().ToConstant(expected2)
                .WhenInjectedExactlyInto<ConfigLoader>();


            var configLoader = kernel.Get<ConfigLoader>();

            configLoader.Commands.Should().HaveCount(2);
        }
    }
}