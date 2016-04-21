namespace NinjectTest.GenericCommandHandling
{
    public interface ICommand
    {
    }

    public class RequestStatus
    {
    }

    public interface ICommandDispatcher
    {
        /// <summary>
        /// Dispatches a command to its handler
        /// </summary>
        /// <typeparam name="TParameter">Command Type</typeparam>
        /// <param name="command">The command to be passed to the handler</param>
        RequestStatus Dispatch<TParameter>(TParameter command)
            where TParameter : ICommand;
    }

}