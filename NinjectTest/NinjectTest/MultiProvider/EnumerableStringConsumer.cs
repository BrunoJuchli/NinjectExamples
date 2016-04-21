
using System.Collections.Generic;

namespace NinjectTest.MultiProvider
{
    public class EnumerableStringConsumer
    {
        public IReadOnlyCollection<string> Strings { get; set; }

        public EnumerableStringConsumer(IReadOnlyCollection<string> strings)
        {
            Strings = strings;
        }
    }
}