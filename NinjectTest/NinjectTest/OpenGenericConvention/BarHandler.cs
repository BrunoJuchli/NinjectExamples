using System.Collections.Generic;

namespace NinjectTest.OpenGenericConvention
{
    public class BarHandler : IHandler<BarRequest>
    {
        public void Handle(IEnumerable<BarRequest> requests)
        {
            throw new System.NotImplementedException();
        }
    }
}