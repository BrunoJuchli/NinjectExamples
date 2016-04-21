using Ninject;
using Xunit;

namespace NinjectExamples
{
    public class ClientId { }

    public class ContextualCtorParameter
    {

        [Fact]
        public void Test()
        {
            var kernel = new StandardKernel();

            //kernel.Bind<string>().ToSelf()
            //    .WithParameter(new TypeMatchingConstructorArgument(
            //        typeof(ClientId),
            //        (ctx, target) => (ClientId)HttpContext.Current.Session["clientId"]));

            //kernel.Bind<string>().ToMethod(ctx => )
        }
    }
}