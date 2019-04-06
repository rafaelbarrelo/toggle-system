using AutoMapper;
using ToggleSystem.Domain.Entities;

namespace ToggleSystem.Domain.DTOs
{
    public class EntityToDtoMappingProfile : Profile
    {
        public EntityToDtoMappingProfile() => CreateMap<Toggle, ToggleDto>();
    }
}
