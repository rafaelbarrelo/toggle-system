using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToggleSystem.Domain.Entities;
using ToggleSystem.Domain.Interfaces.Repositories;
using ToggleSystem.Infra.Data.Context;

namespace ToggleSystem.Infra.Data.Repositories
{
    public class ToggleRepository : BaseRepository, IToggleRepository
    {
        public ToggleRepository(ToggleContext context) : base(context) { }

        public async Task<IEnumerable<Toggle>> GetAll(string client, int toggleVersion = 1)
        {
            var sql = @"SELECT * FROM Toggle WHERE IsDeleted = 0 AND Version = @toggleVersion";

            var result = await ExecuteAsync(connection => connection.QueryAsync<Toggle>(sql, new { toggleVersion }));

            return (result.Success) ? result.Data : new Toggle[] { };
        }
    }
}
