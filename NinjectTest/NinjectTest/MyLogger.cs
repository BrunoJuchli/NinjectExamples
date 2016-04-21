using log4net;
using Ninject;
using Xunit;

namespace NinjectTest
{
    public class MyLogger
    {
        private readonly ILog log;

        public MyLogger(ILog log)
        {
            this.log = log;
        }

        public void Log(string something)
        {
            this.log.Info(something);
        }
    }

    public class LoggerTest
    {
        [Fact]
        public void Test()
        {
            var kernel = new StandardKernel();
            kernel.Get<MyLogger>();
        }
    }
}