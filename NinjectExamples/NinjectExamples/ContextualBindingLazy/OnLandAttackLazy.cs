using System;

namespace NinjectExamples.ContextualBindingLazy
{
    internal class OnLandAttackLazy : IAttack
    {
        private readonly Lazy<IWarrior> warriorFactory;

        public IWarrior Warrior
        {
            get { return warriorFactory.Value; }
        }

        public OnLandAttackLazy(Lazy<IWarrior> warriorFactory)
        {
            this.warriorFactory = warriorFactory;
        }
    }
}