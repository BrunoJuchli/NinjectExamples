using Ninject.Activation;
using System.Collections.Generic;

namespace NinjectTest.MultiProvider
{
    public class EnumerableStringProvider : Provider<IReadOnlyCollection<string>>
    {
        protected override IReadOnlyCollection<string> CreateInstance(IContext context)
        {
            return new[] {"foo", "bar"};
        }
    }
}