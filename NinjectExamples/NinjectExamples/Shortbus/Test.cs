//using Ninject;
//using Ninject.Extensions.Conventions;
//using ShortBus;
//using ShortBus.Ninject;
//using Xunit;

//namespace NinjectExamples.Shortbus
//{
//    public class Test
//    {
//        [Fact]
//        public void TestMethod()
//        {
//            var kernel = new StandardKernel();

//            kernel.Bind(x => x.FromThisAssembly()
//                .SelectAllClasses()
//                .InheritedFromAny(
//                    new[]
//            {
//                typeof(ICommandHandler<>), 
//                typeof(IQueryHandler<,>)
//            })
//                .BindDefaultInterfaces());

//            kernel.Bind<IDependencyResolver>().ToMethod(x => DependencyResolver.Current);

//            kernel.Bind<IDependencyResolver>().ToConstructor(x => new NinjectDependencyResolver(x.Inject<IKernel>()));

//            ShortBus.DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
//        }
//    }
//}