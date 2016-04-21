using FluentAssertions;
using Ninject;
using Xunit;

namespace NinjectTest.SO36057755
{
    public interface IPSCCheckContext
    {
    }

    public class PSCCheckContext : IPSCCheckContext
    {
        public string ImgNamekey { get; set; }
        public string ImgFlagKey { get; set; }
        public string ServerLocationKeyName { get; set; }
        public string AppNamekey { get; set; }
        public string ServerLocationNameKey { get; set; }

        public PSCCheckContext(string appNamekey, string serverLocationNameKey)
        {
            AppNamekey = appNamekey;
            ServerLocationNameKey = serverLocationNameKey;
        }

        public PSCCheckContext(string imgNamekey, string imgFlagKey, string serverLocationKeyName)
        {
            ImgNamekey = imgNamekey;
            ImgFlagKey = imgFlagKey;
            ServerLocationKeyName = serverLocationKeyName;
        }
    }

    public class Test
    {
        [Fact]
        public void Foo()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IPSCCheckContext>().To<PSCCheckContext>()
                .WithConstructorArgument("appNamekey", "Name of Staff Application")
                .WithConstructorArgument("serverLocationNameKey", "Location of Application Server");

            kernel.Get<IPSCCheckContext>().Should().NotBeNull();;
        }
    }
}