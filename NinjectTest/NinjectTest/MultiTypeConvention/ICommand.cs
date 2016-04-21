namespace NinjectTest.MultiTypeConvention
{
    public interface ICommand
    { 
    }

    class AboutCommand : ICommand
    {
    }

    internal class OptionsCommand : ICommand
    {
    }
}