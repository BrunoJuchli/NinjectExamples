using System.Data;

namespace NinjectTest.ConditionalBindingsForLotsOfTypesWithEagerCreation
{
    public interface IExport
    {
        void ExportData(DataTable data);
    }
}