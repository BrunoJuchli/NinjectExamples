using FluentAssertions;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NinjectTest.TryGetMulti
{
    public class Test
    {
        [Fact]
        public void Foo()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IFoo>().To<FooA>();
            kernel.Bind<IFoo>().To<FooWithDependencyD>();
            kernel.Bind<IFoo>().To<FooB>();
            kernel.Bind<IFoo, FooC>().To<FooC>();
            kernel.Bind<IFoo>().To<FooWithDependencyE>();

            kernel.TryGetAll<IFoo>().Should()
                .HaveCount(3)
                .And.Contain(x => x.GetType() == typeof(FooA))
                .And.Contain(x => x.GetType() == typeof(FooB))
                .And.Contain(x => x.GetType() == typeof(FooC));
        }
    }

    public static class ResolutionRootExtensions
    {
        public static IEnumerable<T> TryGetAll<T>(this IResolutionRoot resolutionRoot)
        {
            var request = resolutionRoot.CreateRequest(typeof(IFoo), x => true, Enumerable.Empty<IParameter>(), true, false);
            IEnumerable results = resolutionRoot.Resolve(request);
            IEnumerator enumerator = results.GetEnumerator();

            while (MoveNextIgnoringActivationException(enumerator))
            {
                yield return (T)enumerator.Current;
            }
        }

        private static bool MoveNextIgnoringActivationException(IEnumerator enumerator)
        {
            while (true)
            {
                try
                {
                    return enumerator.MoveNext();
                }
                catch (ActivationException)
                {
                }
            }
        }
    }

    public interface IFoo
    {
    }

    class FooA : IFoo { }

    class FooB : IFoo { }

    class FooC : IFoo { }

    class FooWithDependencyD : IFoo
    {
        private readonly IDependency _dependency;

        public FooWithDependencyD(IDependency dependency)
        {
            _dependency = dependency;
        }
    }

    class FooWithDependencyE : IFoo
    {
        private readonly IDependency _dependency;

        public FooWithDependencyE(IDependency dependency)
        {
            _dependency = dependency;
        }
    }

    internal interface IDependency
    {
    }
}