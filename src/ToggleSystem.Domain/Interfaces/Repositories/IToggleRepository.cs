using System.Collections.Generic;
using System.Threading.Tasks;
using ToggleSystem.Domain.DTOs;

namespace ToggleSystem.Domain.Interfaces.Repositories
{
    public interface IToggleRepository
    {
        Task<IEnumerable<ToggleDto>> GetAll(string client, int toggleVersion = 1);
    }
}
