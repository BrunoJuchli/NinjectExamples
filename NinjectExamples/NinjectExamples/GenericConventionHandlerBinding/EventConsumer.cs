namespace NinjectExamples.GenericConventionHandlerBinding
{
    internal class EventConsumer : IConsume<DeliverCreated>, IConsume<DeliverUpdated>
    {
        public void Consume(DeliverCreated e)
        {
            throw new System.NotImplementedException();
        }

        public void Consume(DeliverUpdated e)
        {
            throw new System.NotImplementedException();
        }
    }
}