using FluentAssertions;
using Ninject;
using Xunit;

namespace NinjectTest.NamedBinding
{
    public class Test
    {
        [Fact]
        public void SomeTest()
        {
            const string string1 = "A";
            const string string2 = "B";
            const string defaultString = "default";

            var kernel = new StandardKernel();

            kernel.Bind<string>().ToConstant(string1).Named(string1)
                .BindingConfiguration.IsImplicit = true;
            kernel.Bind<string>().ToConstant(string2).Named(string2)
                .BindingConfiguration.IsImplicit = true;
            kernel.Bind<string>().ToConstant(defaultString);

            kernel.Get<string>().Should().Be(defaultString);

            kernel.Get<string>(string2).Should().Be(string2);
        }
    }
}