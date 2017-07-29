using Ninject.Extensions.Conventions.BindingGenerators;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NinjectTest.SO40375548
{
    public interface IPage { }

    public class PageService { }

    public class MyBindingGenerator : IBindingGenerator
    {
        public IEnumerable<IBindingWhenInNamedWithOrOnSyntax<object>> CreateBindings(
            Type type,
            IBindingRoot bindingRoot)
        {
            yield return bindingRoot
                .Bind(type)
                .ToMethod(ctx => GetInstance(type));
        }

        public static object GetInstance(Type type)
        {
            MethodInfo method = typeof(PageService).GetMethod("Create");
            return method.MakeGenericMethod(type).Invoke(null, new object[] { null });
        }
    }
}
