using System.Collections.Generic;

namespace NinjectTest.OpenGenericConvention
{
    public class FooHandler : IHandler<FooRequest>
    {
        public void Handle(IEnumerable<FooRequest> requests)
        {
            throw new System.NotImplementedException();
        }
    }
}