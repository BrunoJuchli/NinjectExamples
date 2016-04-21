using FluentAssertions;
using Xunit;

namespace NinjectTest.RegularExpressions
{
    public class Test
    {
        [Fact]
        public void FactMethodName()
        {
            string[] result = System.Text.RegularExpressions.Regex.Split("2.34 3.80\t2.65 1.49\t1.27 1.60", @"\s");
            result.Should().HaveCount(6);
        } 
    }
}