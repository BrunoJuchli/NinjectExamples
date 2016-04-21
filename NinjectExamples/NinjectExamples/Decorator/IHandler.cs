namespace NinjectExamples.Decorator
{
    public interface IHandler<in TFor> where TFor : IRequest
    {
        void Handle(TFor request);
    }
}