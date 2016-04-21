    using FluentAssertions;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using System.Linq;
    using Xunit;

namespace NinjectTest
{
    public interface ISomeView { }

    public interface ISomeOtherView { }

    public interface INotEndingWithViewWord { }

    public class SomePage : ISomeView, ISomeOtherView, INotEndingWithViewWord
    {
    }

    public class Demo
    {
        [Fact]
        public void Test()
        {
            using (var kernel = new StandardKernel())
            {
                kernel.Bind(x => x
                    .FromThisAssembly()
                    .SelectAllClasses()
                    .EndingWith("Page")
                    .BindSelection((type, baseType) => type.GetInterfaces().Where(iface => iface.Name.EndsWith("View"))));

                kernel.Get<ISomeView>().Should().BeOfType<SomePage>();

                kernel.Get<ISomeOtherView>().Should().BeOfType<SomePage>();

                kernel.Invoking(x => x.Get<INotEndingWithViewWord>())
                    .ShouldThrow<ActivationException>();

                kernel.Load(new ConfigurableScopeBindingModule(x => x.InSingletonScope()));
            }
        }
    }
}
