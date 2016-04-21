namespace NinjectExamples.Decorator
{
    public class Bar : IRequest
    {
         
    }

    public class BarHandler : IHandler<Bar>
    {
        public void Handle(Bar request)
        {
            throw new System.NotImplementedException();
        }
    }
}