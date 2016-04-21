using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace NinjectTest.AwaitEndsEarly
{
    public class Test
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async void RunTest()
        {
            _testOutputHelper.WriteLine("before");
            await ShowRequestsVsDaysAsync();
            _testOutputHelper.WriteLine("after");
        } 

        private async Task ShowRequestsVsDaysAsync()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(250);
                    _testOutputHelper.WriteLine(i.ToString(CultureInfo.InvariantCulture));
                }
            });
        }
    }
}