using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.BindingGenerators;
using Ninject.Extensions.Factory;
using Ninject.Syntax;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NinjectTest.NotAllConvention
{
    public class Test
    {
        [Fact]
        public void Foo()
        {
            IKernel kernel = new StandardKernel();

            IList<Type> implementedFactoryInterfaces = new List<Type>();
            kernel.Bind(services => services
                .From(AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Where(a => a.FullName.Contains("MyProject")
                             && !a.FullName.Contains("Tests")))
                .SelectAllClasses()
                .EndingWith("Factory")
                .Where(classFactoryType =>
                {
                    implementedFactoryInterfaces.Add(classFactoryType.GetInterfaces().Single());
                    return true;
                })
                .BindDefaultInterface());


            kernel.Bind(services => services
                .From(AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Where(a => a.FullName.Contains("MyProject")
                             && !a.FullName.Contains("Tests")))
                .SelectAllInterfaces()
                .EndingWith("Factory")
                .Excluding(implementedFactoryInterfaces)
                .BindToFactory());
        }

        [Fact]
        public void Foo2()
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind(services => services
                .From(AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Where(a => a.FullName.Contains("MyProject")
                                && !a.FullName.Contains("Tests")))
                .SelectAllInterfaces()
                .EndingWith("Factory")
                .BindWith<InterfaceAndClassFactoryBindingGenerator>());
        }
    }

    public class InterfaceAndClassFactoryBindingGenerator : IBindingGenerator
    {
        public IEnumerable<IBindingWhenInNamedWithOrOnSyntax<object>> CreateBindings(Type type, IBindingRoot bindingRoot)
        {
            if (!type.IsInterface)
            {
                throw new ArgumentOutOfRangeException("type", type, "is not an interface, but only interfaces are supported");
            }

            Type classImplementingTheFactoryInterface = type.Assembly.GetTypes()
                .Where(t => t.IsClass)
                .SingleOrDefault(type.IsAssignableFrom);

            if (classImplementingTheFactoryInterface == null)
            {
                yield return bindingRoot.Bind(type).ToFactory();
            }
            else
            {
                yield return bindingRoot.Bind(type).To(classImplementingTheFactoryInterface);
            }
        }
    }
}