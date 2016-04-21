using FluentAssertions;
using Ninject;
using Ninject.Extensions.Conventions.BindingGenerators;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NinjectTest.OpenGenericConvention
{
    public class Test
    {
        [Fact]
        public void FactMethodName()
        {
            var kernel = new StandardKernel();

            //kernel.Bind(x => x
            //    .FromThisAssembly()
            //    .SelectAllClasses()
            //    .InheritedFrom(typeof(IHandler<>))
            //    //.Where(IsHandler)
            //    .BindWith<HandlerBindingGenerator>());

            //IHandler<IRequest> df = new FooHandler();

            kernel.Bind(typeof (FooHandler)).To(typeof (IHandler<IRequest>));

            var handlers = kernel.GetAll<IHandler<IRequest>>().ToList();
            
            handlers.Should().HaveCount(2);
        }

        private static bool IsHandler(Type t)
        {
            return t.GetInterfaces()
                .Where(x => x.IsGenericType)
                .Select(x => x.GetGenericTypeDefinition())
                .Any(x => x == typeof (IHandler<>));
        }
    }

    public class HandlerBindingGenerator : IBindingGenerator
    {
        public IEnumerable<IBindingWhenInNamedWithOrOnSyntax<object>> CreateBindings(Type type, IBindingRoot bindingRoot)
        {
            yield return bindingRoot.Bind(type).To(typeof(IHandler<IRequest>));
        }
    }
}