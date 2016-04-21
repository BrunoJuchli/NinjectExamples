namespace NinjectExamples.GenericConventionHandlerBinding
{
    public interface IEvent
    {
    }

    class DeliverUpdated : IEvent
    {
    }

    class DeliverCreated : IEvent
    {
    }

    public interface IConsume<in TEvent>
        where TEvent : IEvent
    {
        void Consume(TEvent e);
    }
}