using Ninject;
using Ninject.Extensions.Conventions;
using Xunit;

namespace NinjectExamples.Decorator
{
    public class Test
    {
        [Fact]
        public void FactMethodName()
        {
            var kernel = new StandardKernel();

            kernel.Bind(typeof(ICompositeHandler<>)).To(typeof(CompositeHandler<>));

            kernel.Bind(x => x.FromThisAssembly()
                .SelectAllClasses()
                .InheritedFrom(typeof(IHandler<>))
                .Excluding(typeof(CompositeHandler<>))
                .BindDefaultInterfaces());

            kernel.Get<ICompositeHandler<Foo>>();
        } 
    }
}