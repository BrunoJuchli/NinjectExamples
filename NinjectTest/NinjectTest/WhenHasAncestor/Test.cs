using FluentAssertions;
using Ninject;
using Xunit;

namespace NinjectTest.WhenHasAncestor
{
    public class MyClass
    {
        public IClientFactory ClientFactory { get; set; }

        public MyClass(IClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }
    }

    public interface IClientFactory
    {
        IResolver Resolver { get; }
    }

    public class ClientFactory : IClientFactory
    {
        public IResolver Resolver { get; set; }

        public ClientFactory(IResolver resolver)
        {
            Resolver = resolver;
        }
    }

    public interface IResolver { }

    public class ResolverA : IResolver { }

    public class ResolverB : IResolver { }

    public class ControllerA
    {
        public MyClass MyClass { get; set; }

        public ControllerA(MyClass myClass)
        {
            MyClass = myClass;
        }
    }

    public class ControllerB
    {
        public MyClass MyClass { get; set; }

        public ControllerB(MyClass myClass)
        {
            MyClass = myClass;
        }
    }

    public class Test
    {
        [Fact]
        public void FactMethodName()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IClientFactory>().To<ClientFactory>();
            kernel.Bind<string>().ToSelf().WhenInjectedInto<string>();
    kernel.Bind<IResolver>()
        .To<ResolverA>()
        .WhenAnyAncestorMatches(x => x.Binding.Service == typeof(ControllerA));

    kernel.Bind<IResolver>()
        .To<ResolverB>()
        .WhenAnyAncestorMatches(x => x.Binding.Service == typeof(ControllerB));

            kernel.Get<ControllerA>().MyClass.ClientFactory.Resolver.Should().BeOfType<ResolverA>();
            kernel.Get<ControllerB>().MyClass.ClientFactory.Resolver.Should().BeOfType<ResolverB>();
        }
    }
}