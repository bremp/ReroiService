using AutoMapper;
using Reroi.Model.Entities;

namespace Reroi.Api.ViewModels.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Country, CountryViewModel>();
            CreateMap<Property, PropertyViewModel>();
        }
    }
}
