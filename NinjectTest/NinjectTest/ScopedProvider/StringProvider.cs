using Ninject.Activation;

namespace NinjectTest.ScopedProvider
{
    public class StringProvider : Provider<string>
    {
        protected override string CreateInstance(IContext context)
        {
            return context.ToString();
        }
    }
}