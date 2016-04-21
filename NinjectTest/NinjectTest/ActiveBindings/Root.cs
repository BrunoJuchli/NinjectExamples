namespace NinjectTest.ActiveBindings
{
    public class Root
    {
        public Mechanism1 Mechanism1 { get; set; }
        public Mechanism2 Mechanism2 { get; set; }

        public Root(Mechanism1 mechanism1,  Mechanism2 mechanism2)
        {
            Mechanism1 = mechanism1;
            Mechanism2 = mechanism2;
        }
    }

    public interface IWheel { }
    public class Wheel1 : IWheel { }
    public class Wheel2 : IWheel { }

    public class Mechanism1
    {
        public IWheel Wheel { get; set; }

        public Mechanism1(IWheel wheel)
        {
            Wheel = wheel;
        }
    }

    public class Mechanism2
    {
        public IWheel Wheel { get; set; }

        public Mechanism2(IWheel wheel)
        {
            Wheel = wheel;
        }
    }
}