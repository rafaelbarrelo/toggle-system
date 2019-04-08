using ToggleSystem.Domain.DTOs;
using ToggleSystem.Domain.Entities;

namespace ToggleSystem.Domain.Extensions
{
    public static class ToggleValueExtensions
    {

        public static bool ToBoolValue(this ToggleDto dto)
        {
            if (dto.ToggleValue.HasValue)
            {
                return dto.ToggleValue.Value == ToggleValue.True;
            }

            return dto.DefaultValue == ToggleValue.True;
        }
    }
}
