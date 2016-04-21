using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Common;
using Ninject;
using Ninject.Extensions.Conventions;
using Xunit;

namespace NinjectExamples.GenericConventionHandlerBinding
{
    public class Test
    {
        [Fact]
        public void Foo()
        {
            var kernel = new StandardKernel();
    kernel.Bind(x => x.FromThisAssembly()
        .IncludingNonePublicTypes()
        .SelectAllClasses()
        .InheritedFrom(typeof(IConsume<>))
        .BindSelection(SelectConsumeInterfacesOnly));

            kernel.Get<IConsume<DeliverCreated>>().Should().BeOfType<EventConsumer>();
        }

    private static IEnumerable<Type> SelectConsumeInterfacesOnly(Type type, IEnumerable<Type> baseTypes)
    {
        var matchingTypes = baseTypes.Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof (IConsume<>));
        return matchingTypes;
    }
    }
}