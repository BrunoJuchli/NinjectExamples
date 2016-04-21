using FluentAssertions;
using Ninject;
using Ninject.Activation;
using Ninject.Syntax;
using System;
using System.Linq;
using Xunit;

namespace NinjectTest.ActiveBindings
{
    public static class NinjectWhenExtensions
    {
        public static IBindingInNamedWithOrOnSyntax<T> WhenIsDescendantOf<T>(this IBindingWhenSyntax<T> syntax, Type ancestor)
        {
            return syntax.When(request => request.ActiveBindings.Any(p => p.Service.Name == ancestor.Name));
        }
    }

    public class Test
    {
        [Fact]
        public void FactMethodName()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IWheel>().To<Wheel1>();
            kernel.Bind<IWheel>().To<Wheel2>().When(IsDescendantOf<Mechanism1>);

            var root = kernel.Get<Root>();
            root.Mechanism1.Wheel.Should().BeOfType<Wheel2>();
            root.Mechanism2.Wheel.Should().BeOfType<Wheel1>();
        }

        private static bool IsDescendantOf<T>(IRequest request)
        {
            return request.ActiveBindings.Any(p => p.Service.Name == typeof(T).Name);
        }
    }
}