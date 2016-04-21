using System.Collections.Generic;

namespace NinjectTest.OpenGenericConvention
{
    public interface IHandler<in TFor>
        where TFor : IRequest
    {
        void Handle(IEnumerable<TFor> requests);
    }
}