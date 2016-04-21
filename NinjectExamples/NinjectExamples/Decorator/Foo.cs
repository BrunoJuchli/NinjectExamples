namespace NinjectExamples.Decorator
{
    public class Foo : IRequest
    { 
    }

    public class FooHandler : IHandler<Foo>
    {
        public void Handle(Foo request)
        {
            throw new System.NotImplementedException();
        }
    }
}