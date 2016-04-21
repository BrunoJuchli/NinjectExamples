namespace NinjectTest.Runners
{
    public interface IRunnerFactory
    {
        // all parameters are passed to the constructor of Runner
        // parameters are matched by name, so make sure they match! ctor(IConfig myconfig) won't work, it must be ctor(IConfig config).
        // but of course you can add more parameters to the ctor and have them injected: ctor(IService1 foo, IControl bar, IConfig config, IService2 foo2)
        Runner Create(IConfig config);
    }
}