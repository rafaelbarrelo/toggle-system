using AutoMapper;
using ToggleSystem.Api.Models.Response;
using ToggleSystem.Domain.DTOs;
using ToggleSystem.Domain.Extensions;

namespace ToggleSystem.Api.Models.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile() => CreateMap<ToggleDto, ToggleResponse>()
            .ForMember(response => response.Value, mce => mce.MapFrom((dto, res) => dto.ToBoolValue()));
    }
}
