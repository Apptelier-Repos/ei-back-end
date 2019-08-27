using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using ei_core.Entities.UserAccountAggregate;
using MediatR;
using SqlKata;
using SqlKata.Compilers;

namespace ei_infrastructure.Data.Queries
{
    /// <summary>
    ///     Query to return all existing user accounts, or an empty sequence if none exist.
    /// </summary>
    public class GetAllUserAccounts : IRequest<IEnumerable<UserAccount>>
    {
    }

    public class GetAllUserAccountsHandler : IRequestHandler<GetAllUserAccounts,
        IEnumerable<UserAccount>>
    {
        private readonly SqlServerCompiler _compiler;
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;

        public GetAllUserAccountsHandler(IDbConnection dbConnection, IMapper mapper)
        {
            _dbConnection = dbConnection;
            _mapper = mapper;
            _compiler = new SqlServerCompiler();
        }

        public async Task<IEnumerable<UserAccount>> Handle(GetAllUserAccounts request,
            CancellationToken cancellationToken)
        {
            var query = new Query("UserAccount");
            var sqlResult = _compiler.Compile(query);

            var userAccountPocos = (await _dbConnection.QueryAsync<POCOs.UserAccount>(sqlResult.Sql)).ToList();
            return _mapper.Map<IEnumerable<UserAccount>>(userAccountPocos);
        }
    }
}