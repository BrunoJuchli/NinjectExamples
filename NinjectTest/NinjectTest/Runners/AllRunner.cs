using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjectTest.Runners
{
    public class AllRunner
    {
        private readonly IList<IConfig> runnerConfigurations;
        private readonly IRunnerFactory runnerFactory;

        public AllRunner(IList<IConfig> runnerConfigurations, IRunnerFactory runnerFactory)
        {
            this.runnerConfigurations = runnerConfigurations;
            this.runnerFactory = runnerFactory;
        }

        public void RunAll()
        {
            Task[] runners = this.runnerConfigurations
                .Select(this.runnerFactory.Create)
                .Select(runner => Task.Run((Action) runner.Run))
                .ToArray();

            Task.WaitAll(runners);
        }
    }
}