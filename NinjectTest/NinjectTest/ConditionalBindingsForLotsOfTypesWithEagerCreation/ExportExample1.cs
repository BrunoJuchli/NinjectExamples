namespace NinjectTest.ConditionalBindingsForLotsOfTypesWithEagerCreation
{
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Xunit;


    public class ExportExample1
    {
        [Fact]
        public void Foo()
        {
            var kernel = new StandardKernel();
            kernel.Bind(s => s.FromThisAssembly()
                    .IncludingNonePublicTypes()
                    .SelectAllClasses()
                    .InheritedFrom<IExport>()
                    .BindSelection((type, baseTypes) => new[] { typeof(IExport) }));

            kernel.Bind<IExportDictionary>().To<ExportDictionary>().InSingletonScope();

            // create the dictionary immediately after the kernel is initialized.
            // do this in the "composition root".
            // why? creation of the dictionary will lead to creation of all `IExport`
            // that means if one cannot be created because a binding is missing (or such)
            // it will fail here (=> fail early).
            var exportDictionary = kernel.Get<IExportDictionary>(); 

            exportDictionary.Get(typeof(ExportOne).FullName).ExportData(null);
        }
    }
}