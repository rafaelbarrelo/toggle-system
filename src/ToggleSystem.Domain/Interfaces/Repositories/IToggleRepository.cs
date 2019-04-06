using System.Collections.Generic;
using System.Threading.Tasks;
using ToggleSystem.Domain.Entities;

namespace ToggleSystem.Domain.Interfaces.Repositories
{
    public interface IToggleRepository
    {
        Task<IEnumerable<Toggle>> GetAll(string client, int toggleVersion = 1);
    }
}
