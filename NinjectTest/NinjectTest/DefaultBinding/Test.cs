using FluentAssertions;
using Ninject;
using Xunit;

namespace NinjectTest.DefaultBinding
{
    public class Test
    {
        [Fact]
        public void RegisterInstance_unnamed_should_return_unnamed_when_multiple_registrations()
        {
            var sut = new StandardKernel();
            var instance1 = new Dependency3();
            var instance2 = new Dependency3();

            sut.Bind<Dependency3>().ToConstant(instance1).Named("instance1")
                .BindingConfiguration.IsImplicit = true;
            sut.Bind<Dependency3>().ToConstant(instance2);

            sut.Get<Dependency3>("instance1").Should().BeSameAs(instance1);
            sut.Get<Dependency3>().Should().BeSameAs(instance2);
        }
    }

    public class Dependency3
    {
    }
}