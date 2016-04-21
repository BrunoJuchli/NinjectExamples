namespace NinjectExamples.Decorator2
{
    public interface ICommand<T> : ICommand where T : class
    {
        void Execute(T args);
    }

    public interface ICommand
    {
    }
}