using FluentAssertions;
using Xunit;

namespace NinjectTest.Repro
{
    using Ninject;
    using Ninject.Modules;

    public class Class1
    {
        [Fact]
        public void SelfModule()
        {
            var kernel = new StandardKernel(new SelfModule());
            kernel.Invoking(x => x.Get<Bar>()).ShouldNotThrow(); // this throws in my ~real~ code.
            // I'll try to figure out what is different but it is nontrivial as the sln is pretty big.
            // The sln is ~5 000 classes if that has anything to do with anything.
            // It is the ProviderCallback that is null and it started throwing when I refactored to using modules.
        }
    }

    public class SelfModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<Foo>().ToSelf().InSingletonScope();
            Kernel.Bind<Bar>().ToSelf().InSingletonScope();
        }
    }

    public class Foo
    {
        internal readonly int Value = 2;
    }

    public class Bar
    {
        public Bar(Foo foo)
        {
        }
    }
}