using FluentAssertions;
using Ninject;
using Ninject.Activation;
using Ninject.Extensions.NamedScope;
using Ninject.Parameters;
using Ninject.Planning.Targets;
using Ninject.Syntax;
using System;
using System.Linq;
using Xunit;

namespace NinjectTest.NonRequestDbContext
{
    public class Test
    {
        // the two implementations are just for demonstration and easy verification purposes. You will only use one DbContext type.
        public interface IFakeDbContext { }
        public class RequestScopeDbContext : IFakeDbContext { }
        public class CallScopeDbContext : IFakeDbContext { }

        public class SomeTask
        {
            public IFakeDbContext FakeDbContext { get; set; }
            public Dependency1 Dependency1 { get; set; }
            public Dependency2 Dependency2 { get; set; }

            public SomeTask(IFakeDbContext fakeDbContext, Dependency1 dependency1, Dependency2 dependency2)
            {
                FakeDbContext = fakeDbContext;
                Dependency1 = dependency1;
                Dependency2 = dependency2;
            }
        }

        public class Dependency1
        {
            public IFakeDbContext FakeDbContext { get; set; }

            public Dependency1(IFakeDbContext fakeDbContext)
            {
                FakeDbContext = fakeDbContext;
            }
        }

        public class Dependency2
        {
            public IFakeDbContext FakeDbContext { get; set; }

            public Dependency2(IFakeDbContext fakeDbContext)
            {
                FakeDbContext = fakeDbContext;
            }
        }

        public class TaskScheduler
        {
            private readonly IResolutionRoot _resolutionRoot;

            public TaskScheduler(IResolutionRoot resolutionRoot)
            {
                _resolutionRoot = resolutionRoot;
            }

            public SomeTask CreateScheduledTaskNow()
            {
                return _resolutionRoot.Get<SomeTask>(new NonRequestScopedParameter());
            }
        }

    public class NonRequestScopedParameter : Ninject.Parameters.IParameter
    {
        public bool Equals(IParameter other)
        {
            if (other == null)
            {
                return false;
            }

            return other is NonRequestScopedParameter;
        }

        public object GetValue(IContext context, ITarget target)
        {
            throw new NotSupportedException("this parameter does not provide a value");
        }

        public string Name
        {
            get { return typeof(NonRequestScopedParameter).Name; }
        }

        // this is very important
        public bool ShouldInherit
        {
            get { return true; }
        }
    }

        [Fact]
        public void FactMethodName()
        {
            var kernel = new StandardKernel();

            // this is the default binding
            kernel.Bind<IFakeDbContext>().To<RequestScopeDbContext>();

            // this binding is _only_ used when the request contains a NonRequestScopedParameter
            // in call scope means, that all objects built in the a single request get the same instance
            kernel.Bind<IFakeDbContext>().To<CallScopeDbContext>()
                .When(x => x.Parameters.OfType<NonRequestScopedParameter>().Any())
                .InCallScope();

            // let's try it out!
            var task = kernel.Get<SomeTask>(new NonRequestScopedParameter());

            // verify that the correct binding was used
            task.FakeDbContext.Should().BeOfType<CallScopeDbContext>();

            // verify that all children of the task get injected the same task instance
            task.FakeDbContext.Should()
                .Be(task.Dependency1.FakeDbContext)
                .And.Be(task.Dependency2.FakeDbContext);
        }

        [Fact]
        public void TestWithoutExplicitResolution()
        {
            var kernel = new StandardKernel();

            kernel.Bind<SomeTask>().ToSelf().WithParameter(new NonRequestScopedParameter());

            // this is the default binding
            kernel.Bind<IFakeDbContext>().To<RequestScopeDbContext>();

            // this binding is _only_ used when the request contains a NonRequestScopedParameter
            // in call scope means, that all objects built in the a single request get the same instance
            kernel.Bind<IFakeDbContext>().To<CallScopeDbContext>()
                .When(x => x.Parameters.OfType<NonRequestScopedParameter>().Any())
                .InCallScope();

            // let's try it out!
            var task = kernel.Get<SomeTask>();

            // verify that the correct binding was used
            task.FakeDbContext.Should().BeOfType<CallScopeDbContext>();

            // verify that all children of the task get injected the same task instance
            task.FakeDbContext.Should()
                .Be(task.Dependency1.FakeDbContext)
                .And.Be(task.Dependency2.FakeDbContext);
        }
    }
}