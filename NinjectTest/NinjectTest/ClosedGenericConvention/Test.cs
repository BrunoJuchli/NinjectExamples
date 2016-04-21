    using FluentAssertions;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Xunit;

    namespace NinjectTest.ClosedGenericConvention
    {
        public interface ICommand<TParam> { }

        public interface IFloatCommand : ICommand<float> { }

        public class FloatCommand : IFloatCommand { }

        public class IntCommand : ICommand<int> { }

        public class Test
        {
            [Fact]
            public void Fact()
            {
                var kernel = new StandardKernel();

                kernel.Bind(x => x.FromThisAssembly()
                    .IncludingNonePublicTypes()
                    .SelectAllClasses()
                    .InheritedFrom(typeof(ICommand<>))
                    .BindDefaultInterfaces()
                    .Configure(b => b.InSingletonScope()));

                kernel.Get<IFloatCommand>().Should().BeOfType<FloatCommand>();
                kernel.Get<ICommand<int>>().Should().BeOfType<IntCommand>();
            }
        }
    }