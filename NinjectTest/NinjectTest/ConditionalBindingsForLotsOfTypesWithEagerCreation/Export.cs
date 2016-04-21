using System.Data;

namespace NinjectTest.ConditionalBindingsForLotsOfTypesWithEagerCreation
{
    internal class ExportOne : IExport
    {
        public void ExportData(DataTable data)
        {
            throw new System.NotImplementedException();
        }
    }
}