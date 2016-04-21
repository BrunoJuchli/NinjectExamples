namespace NinjectTest.ChildKernelRebind
{
    public interface IServiceProvider
    {
        IService Provide();
    }

    public class ServiceProvider : IServiceProvider
    {
        private readonly IService _service;

        public ServiceProvider(IService service)
        {
            _service = service;
        }

        public IService Provide()
        {
            return _service;
        }
    }
}