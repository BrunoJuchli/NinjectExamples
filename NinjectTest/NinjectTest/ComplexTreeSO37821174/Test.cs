using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Xunit;
using Ninject.Parameters;

namespace NinjectTest.ComplexTreeSO37821174
{
    public class Test
    {
        [Fact]
        public void Example()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IB>().To<B>()
                .WithParameter(
                    new TypeMatchingConstructorArgument(
                        typeof(IC), 
                        (ctx, target) => ctx.Kernel.Get<C1>()));
            kernel.Bind<IB>().To<B>()
                .WithParameter(
                    new TypeMatchingConstructorArgument(
                        typeof(IC),
                        (ctx, target) => ctx.Kernel.Get<C2>()));
            kernel.Bind<IA>().To<A>();

            var a = kernel.Get<IA>();
        }
    }

    public class A : IA
    {
        public A(IB[] bs) { /* ... */ }
    }

    public class B : IB
    {
        public B(IC c) { /* ... */ }
    }

    public class C1 : IC
    {
        public C1() { /* ... */ }
    }

    public class C2 : IC
    {
        public C2() { /* ... */ }
    }

    public interface IA { }
    public interface IB { }
    public interface IC { }
}
