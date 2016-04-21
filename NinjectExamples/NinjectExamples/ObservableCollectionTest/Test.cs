namespace NinjectExamples.ObservableCollectionTest
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using FluentAssertions;

    using Ninject;

    using Xunit;

    public class Test
    {
        [Fact]
        public void FactMethodName()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IComponent>().To<Component1>();
            kernel.Bind<IComponent>().To<Component2>();

            //kernel.Bind<ObservableCollection<IComponent>>()
            //    .ToConstructor(x => new ObservableCollection<IComponent>(x.Inject<IList<IComponent>>()));

            kernel.Bind<ObservableCollection<IComponent>>()
                .ToMethod(x => new ObservableCollection<IComponent>(x.Kernel.GetAll<IComponent>()));

            

            var collection = kernel.Get<ObservableCollection<IComponent>>();

            collection.Should().HaveCount(2);
        }
    }
}