using Ninject;
using Xunit;

namespace NinjectTest.CtorInjectBinding
{
    interface IFoo { }

    class Foo : IFoo { }

    class Bar
    {
        public Bar(Foo foo) { }
    }


    public class Test
    {
        [Fact]
        public void Foo()
        {
            var kernel = new StandardKernel();

            //kernel.Bind<IFoo>().To<Foo>();

            kernel.Bind<Bar>().ToConstructor(ctx => new Bar(ctx.Inject<Foo>()));

            kernel.Get<Bar>();
        }
    }
    
}