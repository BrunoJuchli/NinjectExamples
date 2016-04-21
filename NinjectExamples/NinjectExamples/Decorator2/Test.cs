using FluentAssertions;
using Ninject;
using Xunit;

namespace NinjectExamples.Decorator2
{
    public class Test
    {
        [Fact]
        public void Foo()
        {
            var kernel = new StandardKernel();

            kernel.Bind<ICommand<ChangePasswordArgs>>().To<ChangePasswordCommand>()
                .WhenInjectedInto<TransactionalCommand<ChangePasswordArgs>>();
            kernel.Bind<ICommand<ChangePasswordArgs>>().To<TransactionalCommand<ChangePasswordArgs>>()
                .InTransientScope();

            var command = kernel.Get<ICommand<ChangePasswordArgs>>();

            command.Should().BeOfType<TransactionalCommand<ChangePasswordArgs>>();
        }
    }
}