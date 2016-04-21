using FluentAssertions;
using Ninject;
using Ninject.Activation;
using Ninject.Parameters;
using System;
using Xunit;

namespace NinjectTest.KnowWhereInjected
{
    public class Test
    {
        [Fact]
        public void Foo()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Service>().ToSelf();

            kernel.Bind<Logger>().ToSelf()
                .When(MatchThis)
                .WithParameter(new TypeMatchingConstructorArgument(typeof(string), (ctx, f) => "foo"))
                .WithConstructorArgument(typeof(Type), ctx => ctx.Request.Target.Member.DeclaringType);

            var service = kernel.Get<Service>();

            service.Logger.RootService.Should().Be(typeof(Service));
        }

        private bool MatchThis(IRequest arg)
        {
            return false;
        }
    }

    public class Logger
    {
        private readonly Type _rootService;
        
        public Logger(Type rootService)
        {
            _rootService = rootService;
        }

        public Type RootService
        {
            get { return _rootService; }
        }
    }

    public class Service
    {
        public Logger Logger { get; private set; }

        public Service(Logger logger)
        {
            Logger = logger;
        }
    }
}