using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using ToggleSystem.Infra.Data.Context;

namespace ToggleSystem.Infra.Data.Repositories
{
    public class SqlExecutionResult<TData>
    {
        public TData Data { get; set; }
        public bool Success { get; set; }
        public Exception Exception { get; set; }
        public long ElapsedMilliseconds { get; set; }
    }

    public abstract class BaseRepository
    {
        private readonly string _connectionString;

        protected BaseRepository(ToggleContext context) => _connectionString = context.Database.GetDbConnection().ConnectionString;

        protected async Task<SqlExecutionResult<T>> ExecuteAsync<T>(Func<IDbConnection, Task<T>> function)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var stopwatch = new Stopwatch();
                try
                {
                    stopwatch.Start();
                    await connection.OpenAsync();

                    var result = await function(connection);

                    return new SqlExecutionResult<T>
                    {
                        Success = true,
                        Data = result,
                        Exception = null,
                        ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
                    };
                }
                catch (Exception ex)
                {
                    return new SqlExecutionResult<T>
                    {
                        Success = false,
                        Data = default,
                        Exception = ex,
                        ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
                    };
                }
            }
        }
    }
}
