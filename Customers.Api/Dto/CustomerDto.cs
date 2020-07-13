namespace Customers.Api.Dto
{
    using System;

    /// <summary>
    ///  Customer details
    /// </summary>
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}