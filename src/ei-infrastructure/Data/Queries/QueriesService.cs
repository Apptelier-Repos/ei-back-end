using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using SqlKata;
using SqlKata.Compilers;

namespace ei_infrastructure.Data.Queries
{
    public class QueriesService
    {
        private readonly IDbConnection _connection;
        private readonly SqlServerCompiler _compiler;

        public QueriesService(IDbConnection connection)
        {
            _connection = connection;
            _compiler = new SqlServerCompiler();
        }

        public async Task<DTOs.UserAccount> GetUserAccountByIdAsync(int id)
        {
            var query = new Query("UserAccount")
                .Where("Id", id);
            var sqlResult = _compiler.Compile(query);
            DTOs.UserAccount result;
            try
            {
                result = await _connection.QuerySingleAsync<DTOs.UserAccount>(sqlResult.Sql, new { p0 = sqlResult.Bindings[0] });
            }
            catch (InvalidOperationException e) when (e.Message.IndexOf("no elements", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return null;
            }

            return result;
        }
    }
}