namespace Customers.Application.Dto
{
    using System;

    public class CustomerDto
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public CustomerDto(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}