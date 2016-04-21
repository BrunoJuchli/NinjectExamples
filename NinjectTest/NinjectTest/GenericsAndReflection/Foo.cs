using Ninject;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace NinjectTest.GenericsAndReflection
{
    public interface IRepository<T>
    {
        void DoSomething();
    }

    internal class Repository<T> : IRepository<T>
    {
        public void DoSomething()
        {
            Console.WriteLine("Doing something with Repository<{0}>", typeof(T).Name);
        }
    }

    public class Test
    {
        [Fact]
        public void TestIt()
        {
            var kernel = new StandardKernel();
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));

            var foo = kernel.Get<Foo>();

            foo.DoStuffToRepositories(typeof(string), typeof(int));
        }
    }

    internal class Foo
    {
        private static readonly MethodInfo DoStuffToRepositoryForMethod = typeof(Foo).GetMethod(
            "DoStuffToRepositoryFor",
            BindingFlags.Instance | BindingFlags.NonPublic);

        private readonly IResolutionRoot resolutionRoot;

        public Foo(IResolutionRoot resolutionRoot)
        {
            this.resolutionRoot = resolutionRoot;
        }

        public void DoStuffToRepositories(params Type[] entityTypes)
        {
            foreach (Type entityType in entityTypes)
            {
                MethodInfo doStuffMethod = DoStuffToRepositoryForMethod.MakeGenericMethod(entityType);
                doStuffMethod.Invoke(this, new object[0]);
            }
        }

        private void DoStuffToRepositoryFor<T>()
        {
            var repository = this.resolutionRoot.Get<IRepository<T>>();
            repository.DoSomething();
        }
    }
}