using Moq;
using Ninject;
using Ninject.Activation;
using Ninject.Extensions.DependencyCreation;
using System;
using Xunit;

namespace NinjectExamples.DependencyCreationExample
{
    public class Test
    {
        private readonly Mock<ISingletonDependency> _dependency;

        public Test()
        {
            _dependency = new Mock<ISingletonDependency>();
        }

        [Fact]
        public void Foo()
        {
            var kernel = new StandardKernel();
            kernel.Bind<string>().ToConstant("hello");

            kernel.Bind<ISingletonDependency>().ToMethod(CreateDependency)
                .InSingletonScope()
                .OnActivation(x => x.Initialize());
            kernel.DefineDependency<string, ISingletonDependency>();

            kernel.Get<string>();
            kernel.Get<string>();
            kernel.Get<string>();

            _dependency.Verify(x => x.Initialize(), Times.Exactly(1));
            _dependency.Verify(x => x.Dispose(), Times.Never);

            kernel.Dispose();

            _dependency.Verify(x => x.Dispose(), Times.Exactly(1));
        }

        private ISingletonDependency CreateDependency(IContext arg)
        {
            return _dependency.Object;
        }
    }

    public interface ISingletonDependency : IDisposable
    {
        void Initialize();
    }
}