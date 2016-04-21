namespace NinjectTest.MultiNamedWrapperAlternative
{
    public interface ITable
    {
        string Name { get; } 
    }

    internal class Table : ITable
    {
        public Table(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}