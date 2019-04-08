using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToggleSystem.Domain.DTOs;
using ToggleSystem.Domain.Entities;
using ToggleSystem.Domain.Interfaces.Repositories;
using ToggleSystem.Infra.Data.Context;

namespace ToggleSystem.Infra.Data.Repositories
{
    public class ToggleRepository : BaseRepository, IToggleRepository
    {
        public ToggleRepository(ToggleContext context) : base(context) { }

        public async Task<IEnumerable<ToggleDto>> GetAll(string client, int toggleVersion = 1)
        {
            var sql = @"
                        SELECT
                            t.Id
                            ,t.[Version]
                            ,t.Name
                            ,t.DefaultValue
                            ,tu.ToggleValue
                        FROM
                            Toggles t 
                            LEFT JOIN ToggleUsers tu ON 
                                (t.Id = tu.ToggleId OR tu.Id is null)
                                and (tu.UserId is null OR tu.UserId = (select TOP 1 Id from aspnetusers where UserName = @client))
                        WHERE   
                            t.IsDeleted = 0
                            AND t.[Version] = @toggleVersion;";

            var result = await ExecuteAsync(connection => connection.QueryAsync<ToggleDto>(sql, new { toggleVersion, client }));

            return (result.Success) ? result.Data : new ToggleDto[] { };
        }
    }
}
