namespace NinjectTest.MultiConfiguredBinding
{
    public interface IFoo { }

    class Foo1 : IFoo
    {
        public Foo1(IBar bar) { }
    }

    class Foo2 : IFoo
    {
        public Foo2(IBar bar, bool theParametersName) { }
    }
}