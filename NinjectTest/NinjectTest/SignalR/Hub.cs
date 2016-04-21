using Ninject;
using Xunit;

namespace NinjectTest.SignalR
{
    public class Hub
    {
    }

    class FooHuub : Hub
    {
        private readonly IDependency _dependency;

        public FooHuub(IDependency dependency)
        {
            _dependency = dependency;
        }
    }

    public interface IDependency
    {
    }

    public class Dependency : IDependency
    {
        private readonly IHttpContext _context;

        public Dependency(IHttpContext context)
        {
            _context = context;
        }
    }

    public class Test
    {
        [Fact]
        public void DoTest()
        {
            var kernel = new StandardKernel();

            kernel.Bind<FooHuub>().ToSelf();

            kernel.Bind<IDependency>().To<Dependency>();

            kernel.Bind<IHttpContext>().ToMethod(ctx => new HttpContext())
                .WhenAnyAncestorMatches(ctx => typeof(Hub).IsAssignableFrom(ctx.Plan.Type));

            kernel.Get<FooHuub>();
        }
    }
}