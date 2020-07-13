namespace Customers.Domain.Common
{
    using DataRepositoryCore;
    using System;

    /// <summary>
    /// Base class for entities keyer by Guid
    /// </summary>
    public class KeyedEntity : Entity, IEntity<Guid>
    {
        public Guid Id { get; set; }
    }
}