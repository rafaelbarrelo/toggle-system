using System.Collections.Generic;
using System.Threading.Tasks;
using ToggleSystem.Domain.DTOs;

namespace ToggleSystem.Domain.Interfaces.Services
{
    public interface IToggleService
    {
        Task<IEnumerable<ToggleDto>> GetAll(string client, int toggleVersion);
    }
}
