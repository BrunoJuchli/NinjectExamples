namespace NinjectExamples.Decorator2
{
    public class TransactionalCommand<T> : ICommand<T>
        where T : class
    {
        private readonly ICommand<T> command;

        public TransactionalCommand(ICommand<T> command)
        {
            this.command = command;
        }

        public void Execute(T args)
        {
            this.command.Execute(args);
        }
    }
}