namespace Customers.Domain.Model
{
    using Customers.Domain.Common;

    /// <summary>
    /// Class defining a customer
    /// </summary>
    public class Customer : KeyedEntity
    {
        public string Name { get; set; }
    }
}