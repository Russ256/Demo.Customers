namespace Customers.Api
{
    using AutoMapper;
    using Customers.Api.Dto;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Dto mappers
            this.CreateMap<Application.Dto.CustomerDto, CustomerDto>();
        }
    }
}