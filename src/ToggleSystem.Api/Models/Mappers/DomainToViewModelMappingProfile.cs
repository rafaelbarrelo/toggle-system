using AutoMapper;
using ToggleSystem.Api.Models.Response;
using ToggleSystem.Domain.DTOs;

namespace ToggleSystem.Api.Models.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile() => CreateMap<ToggleDto, ToggleResponse>();
    }
}
