namespace NinjectTest.MultiNamedWrapperAlternative
{
    public interface ITableWrapper
    {
        ITable Table { get; } 
    }

    internal class TableWrapper : ITableWrapper
    {
        public TableWrapper(ITable table)
        {
            Table = table;
        }

        public ITable Table { get; private set; }
    }
}