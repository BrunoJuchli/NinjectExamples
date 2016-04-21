using FluentAssertions;
using Ninject;
using Ninject.Activation;
using Ninject.Planning.Bindings;
using Ninject.Web.Common;
using System;
using System.Linq;
using Xunit;

namespace WebTest.NinjectTests
{
    public class VerifyWhetherBindingIsInRequestScope
    {
        [Fact]
        public void Foo()
        {
            Func<IContext, object> inRequestScopeCallback = RetrieveInRequestScopeCallback();

            var kernel = new StandardKernel();
            kernel.Bind<string>().ToSelf().InRequestScope();

            IBinding binding = kernel.GetBindings(typeof(string)).Single();
            binding.ScopeCallback.Should().Be(inRequestScopeCallback);
        }

        private Func<IContext, object> RetrieveInRequestScopeCallback()
        {
            using (var kernel = new StandardKernel())
            {
                kernel.Bind<object>().ToSelf().InRequestScope();
                return kernel.GetBindings(typeof(object)).Single().ScopeCallback;
            }
        }
    }
}