using FluentAssertions;
using Ninject;
using Ninject.Activation;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NinjectExamples.MultipleWhenInjectedIntoConditions
{
    public static class WhenExtensions
    {
        public static IBindingInNamedWithOrOnSyntax<T> WhenInjectedInto<T>(this IBindingWhenSyntax<T> syntax, params Type[] types)
        {
            var conditions = ComputeMatchConditions(syntax, types).ToArray();
            return syntax.When(request => conditions.Any(condition => condition(request)));
        }

        private static IEnumerable<Func<IRequest, bool>> ComputeMatchConditions<T>(IBindingWhenSyntax<T> syntax, Type[] types)
        {
            foreach (Type type in types)
            {
                syntax.WhenInjectedInto(type);
                yield return syntax.BindingConfiguration.Condition;
            }
        }
    }

    public class Test
    {
        [Fact]
        public void Foo()
        {
            var kernel = new StandardKernel();

            kernel.Bind<string>().ToConstant("Hello")
                .WhenInjectedInto(typeof(SomeTypeA), typeof(SomeTypeB));

            kernel.Bind<string>().ToConstant("Goodbye")
                .WhenInjectedInto<SomeTypeC>();

            kernel.Get<SomeTypeA>().S.Should().Be("Hello");
            kernel.Get<SomeTypeB>().S.Should().Be("Hello");

            kernel.Get<SomeTypeC>().S.Should().Be("Goodbye");
        }
    }

    public abstract class SomeType
    {
        public string S { get; private set; }

        protected SomeType(string s)
        {
            S = s;
        }
    }

    public class SomeTypeA : SomeType
    {
        public SomeTypeA(string s) : base(s) { }
    }

    public class SomeTypeB : SomeType
    {
        public SomeTypeB(string s) : base(s) { }
    }

    public class SomeTypeC : SomeType
    {
        public SomeTypeC(string s) : base(s) { }
    }
}