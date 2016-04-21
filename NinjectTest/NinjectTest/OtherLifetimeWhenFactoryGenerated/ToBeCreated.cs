using System;

namespace NinjectTest.OtherLifetimeWhenFactoryGenerated
{
    public class ToBeCreated
    {
        private readonly Guid id;

        public ToBeCreated()
        {
            this.id = Guid.NewGuid();
        }

        public Guid Id { get { return this.id; } }

        public override string ToString()
        {
            return id.ToString();
        }
    }
}