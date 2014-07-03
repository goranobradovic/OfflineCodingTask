namespace Implementation.Models
{
    using System;

    public class ModelWithIdentity
    {
        public ModelWithIdentity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}