using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using UserAccountAggregate = ei_core.Entities.UserAccountAggregate;
using MediatR;
using SqlKata;
using SqlKata.Compilers;

namespace ei_infrastructure.Data.Queries
{
    public class GetAllUserAccountsQuery : IRequest<IEnumerable<UserAccountAggregate.UserAccount>>
    {
    }

    public class GetAllUserAccountsQueryHandler : IRequestHandler<GetAllUserAccountsQuery,
        IEnumerable<UserAccountAggregate.UserAccount>>
    {
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;
        private readonly SqlServerCompiler _compiler;

        public GetAllUserAccountsQueryHandler(IDbConnection dbConnection, IMapper mapper, SqlServerCompiler compiler)
        {
            _dbConnection = dbConnection;
            _mapper = mapper;
            _compiler = compiler;
        }

        public async Task<IEnumerable<UserAccountAggregate.UserAccount>> Handle(GetAllUserAccountsQuery request,
            CancellationToken cancellationToken)
        {
            var query = new Query("UserAccount");
            var sqlResult = _compiler.Compile(query);

            var resultDtos = (await _dbConnection.QueryAsync<UserAccountAggregate.UserAccount>(sqlResult.Sql)).ToList();
            return _mapper.Map<IEnumerable<UserAccountAggregate.UserAccount>>(resultDtos);
        }
    }
}