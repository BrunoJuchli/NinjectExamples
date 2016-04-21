namespace NinjectTest.MethodInterceptionByAttribute
{
    public interface IService
    {
        void Intercepted();

        void NotIntercepted();
    }
}