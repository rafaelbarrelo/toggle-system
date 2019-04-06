using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToggleSystem.Domain.DTOs;
using ToggleSystem.Domain.Interfaces.Repositories;
using ToggleSystem.Domain.Interfaces.Services;

namespace ToggleSystem.Domain.Services
{
    public class ToggleService : IToggleService
    {
        private readonly IMapper _mapper;
        private readonly IToggleRepository _toggleRepository;

        public ToggleService(IMapper mapper, IToggleRepository toggleRepository)
        {
            _mapper = mapper;
            _toggleRepository = toggleRepository;
        }

        public async Task<IEnumerable<ToggleDto>> GetAll(string client, int toggleVersion)
        {
            var toggles = await _toggleRepository.GetAll(client, toggleVersion);
            return _mapper.Map<IEnumerable<ToggleDto>>(toggles);
        }
    }
}
