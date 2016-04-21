namespace NinjectTest.MultiNamedWrapperAlternative
{
    public interface ITableProvider
    {
        ITable Open(string name);
    }

    internal class TableProvider : ITableProvider
    {
        public ITable Open(string name)
        {
            return new Table(name);
        }
    }
}