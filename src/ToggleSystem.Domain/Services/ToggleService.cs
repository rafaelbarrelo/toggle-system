using System.Collections.Generic;
using System.Threading.Tasks;
using ToggleSystem.Domain.DTOs;
using ToggleSystem.Domain.Interfaces.Repositories;
using ToggleSystem.Domain.Interfaces.Services;

namespace ToggleSystem.Domain.Services
{
    public class ToggleService : IToggleService
    {
        private readonly IToggleRepository _toggleRepository;

        public ToggleService(IToggleRepository toggleRepository) => _toggleRepository = toggleRepository;

        public async Task<IEnumerable<ToggleDto>> GetAll(string client, int toggleVersion)
        {
            var toggles = await _toggleRepository.GetAll(client, toggleVersion);
            return FilterToggles(toggles);
        }

        private static IEnumerable<ToggleDto> FilterToggles(IEnumerable<ToggleDto> toggles)
        {
            var toggleResult = new List<ToggleDto>();

            foreach (var toggle in toggles)
            {
                if (toggle.DefaultValue == Entities.ToggleValue.Exclusive && !toggle.ToggleValue.HasValue)
                {
                    continue;
                }

                if (toggle.DefaultValue == Entities.ToggleValue.Excluded || (toggle.ToggleValue.HasValue && toggle.ToggleValue.Value == Entities.ToggleValue.Excluded))
                {
                    continue;
                }

                toggleResult.Add(toggle);
            }

            return toggleResult;
        }

        //public async Task AddToggle(ToggleDto toggle)
        //{
        //    // Add new toggle and send notification to broadcast clients
        //}
    }
}
