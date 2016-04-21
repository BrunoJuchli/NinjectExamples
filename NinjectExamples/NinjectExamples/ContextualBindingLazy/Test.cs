using FluentAssertions;
using Ninject;
using Ninject.Modules;
using Xunit;

namespace NinjectExamples.ContextualBindingLazy
{
    public class Test
    {
        [Fact]
        public void FactMethodName()
        {
            var kernel = new StandardKernel();
            kernel.Load(new WarriorModule());

            var amphibious = kernel.Get<IAttack>("amphibious");
            var onLand = kernel.Get<IAttack>("onLand");
            var onLandLazy = kernel.Get<IAttack>("onLandLazy");

            amphibious.Warrior.Should().BeOfType<SpecialNinja>();
            onLand.Warrior.Should().BeOfType<Samurai>();
            onLandLazy.Warrior.Should().BeOfType<Samurai>();
        }
    }

    public class WarriorModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWarrior>().To<Samurai>().WhenInjectedInto<OnLandAttack>();
            Bind<IWarrior>().To<Samurai>().WhenInjectedInto<OnLandAttackLazy>();
            Bind<IWarrior>().To<SpecialNinja>(); // <-- for everything else

            Bind<IAttack>().To<AmphibiousAttack>().Named("amphibious");
            Bind<IAttack>().To<OnLandAttack>().Named("onLand");
            Bind<IAttack>().To<OnLandAttackLazy>().Named("onLandLazy");
        }
    }
}