
namespace NinjectExamples
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Reflection;

    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Extensions.Conventions.BindingGenerators;
    using Ninject.Syntax;

    using Xunit;

    public interface IFoo { }

    [Export(typeof(IFoo))]
    public class Foo : IFoo
    {
    }

    public class ExportBindingGenerator : IBindingGenerator
    {
        public IEnumerable<IBindingWhenInNamedWithOrOnSyntax<object>> CreateBindings(Type type, IBindingRoot bindingRoot)
        {
            foreach (ExportAttribute attribute in type.GetCustomAttributes<ExportAttribute>())
            {
                yield return bindingRoot.Bind(attribute.ContractType).To(type);
            }
        }
    }

    public class Test
    {
        [Fact]
        public void FactMethodName()
        {
            using (var kernel = new StandardKernel())
            {
                kernel.Bind(x => x
                    .FromThisAssembly()
                    .SelectAllClasses()
                    .WithAttribute<ExportAttribute>()
                    .BindWith<ExportBindingGenerator>());

                kernel.Get<IFoo>();
            }
        }
    }
}
