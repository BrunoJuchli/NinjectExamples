
namespace NinjectTest.DefaultAndAlternateBindingConvention.B
{
    public class QueryHandler : IQueryHandler<int, int> { }

    public class AlternativeOneQueryHandler : IQueryHandler<int, int> { }

    public class AlternativeTwoQueryHandler : IQueryHandler<int, int> { }
}