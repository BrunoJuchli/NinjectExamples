namespace NinjectExamples.ContextualBindingLazy
{
    internal class OnLandAttack : IAttack
    {
        public OnLandAttack(IWarrior warrior)
        {
            Warrior = warrior;
        }

        public IWarrior Warrior { get; private set; }
    }
}