using FluentAssertions;
using Ninject;
using Ninject.Extensions.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NinjectTest.ClosedGenericWithBaseBindingConvention
{
    public abstract class EntityBase { }

    public class FooEntity : EntityBase { }

    public class BarEntity : EntityBase { }

    public interface IRepository<out TEntity>
        where TEntity : EntityBase { }

    public class FooRepository : IRepository<FooEntity> { }

    public class BarRepository : IRepository<BarEntity> { }

    public class TypeRequiringAllRepositories
    {
        private readonly ICollection<IRepository<EntityBase>> _repositories;

        public TypeRequiringAllRepositories(ICollection<IRepository<EntityBase>> repositories)
        {
            _repositories = repositories;
        }
    }

    public class Test
    {
        [Fact]
        public void MyTest()
        {
            var kernel = new StandardKernel();

            kernel.Bind(x => x.FromThisAssembly()
                .SelectAllClasses()
                .InheritedFrom(typeof(IRepository<>))
                .BindSelection(this.SelectDefaultInterfaceAndRepositoryBaseInterface));

            kernel.Get<IRepository<FooEntity>>().Should().BeOfType<FooRepository>();

            kernel.GetAll<IRepository<EntityBase>>()
                .Should().HaveCount(2);

            kernel.Get<TypeRequiringAllRepositories>();
        }

        private IEnumerable<Type> SelectDefaultInterfaceAndRepositoryBaseInterface(Type t, IEnumerable<Type> baseTypes)
        {
            yield return baseTypes.Single(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IRepository<>));
            yield return typeof(IRepository<EntityBase>);
        }
    }
}