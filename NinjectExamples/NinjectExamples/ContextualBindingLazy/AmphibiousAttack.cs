namespace NinjectExamples.ContextualBindingLazy
{
    internal class AmphibiousAttack : IAttack
    {
        public AmphibiousAttack(IWarrior warrior)
        {
            Warrior = warrior;
        }

        public IWarrior Warrior { get; private set; }
    }
}