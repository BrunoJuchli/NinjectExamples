using FluentAssertions;
using Ninject;
using Xunit;

namespace NinjectTest.MultiNamedWrapperAlternative
{
    public class Test
    {
        public static class Tables
        {
            public const string FooTable = "Foo";
            public const string BarTable = "Bar";
        }

        [Fact]
        public void TestMethod()
        {
            var kernel = new StandardKernel();
            kernel.Bind<ITableProvider>().ToConstant(new TableProvider());
            kernel.Bind<ITableWrapper>().ToProvider<TableWrapperProvider>();

            kernel.Get<FooTableUser>().TableWrapper.Table.Name.Should().Be(Tables.FooTable);
            kernel.Get<BarTableUser>().TableWrapper.Table.Name.Should().Be(Tables.BarTable);
        }

        public class FooTableUser
        {
            public FooTableUser([TableId(Tables.FooTable)] ITableWrapper tableWrapper)
            {
                TableWrapper = tableWrapper;
            }

            public ITableWrapper TableWrapper { get; private set; }
        }

        public class BarTableUser
        {
            public BarTableUser([TableId(Tables.BarTable)] ITableWrapper tableWrapper)
            {
                TableWrapper = tableWrapper;
            }

            public ITableWrapper TableWrapper { get; private set; }
        }
    }
}