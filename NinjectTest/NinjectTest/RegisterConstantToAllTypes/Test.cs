using FluentAssertions;
using Ninject;
using Ninject.Infrastructure.Language;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NinjectTest.RegisterConstantToAllTypes
{
    public class Test
    {
        [Fact]
        public void FactMethodName()
        {
            var kernel = new StandardKernel();
            var foo = new Foo();
            RegisterConstantAsAllTypes(kernel, foo);

            kernel.Get<IFoo>().Should().Be(foo);
            kernel.Get<Foo>().Should().Be(foo);
            kernel.Get<AbstractFoo>().Should().Be(foo);
        }

        public static void RegisterConstantAsAllTypes(IBindingRoot bindingRoot, object instance)
        {
            Type t = instance.GetType();

            IEnumerable<Type> typesToBind = t.GetAllBaseTypes()
                .Concat(t.GetInterfaces())
                .Except(new[] { typeof(object) });

            bindingRoot.Bind(typesToBind.ToArray()).ToConstant(instance);
        }
    }
}