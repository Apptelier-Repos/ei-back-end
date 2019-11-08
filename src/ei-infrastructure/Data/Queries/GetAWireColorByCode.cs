using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using Dapper;
using ei_core.Entities.WireColorAggregate;
using MediatR;
using SqlKata.Compilers;

namespace ei_infrastructure.Data.Queries
{
    public class GetAWireColorByCode
    {
        public class Query : IRequest<WireColor>
        {
            public string Code { get; set; }

            public Query(string code)
            {
                Guard.Against.NullOrWhiteSpace(code, nameof(code));
                Code = code;
            }
        }

        public class GetAWireColorByCodeHandler : IRequestHandler<Query, WireColor>
        {
            private readonly SqlServerCompiler _compiler;
            private readonly IDbConnection _dbConnection;
            private readonly IMapper _mapper;

            public GetAWireColorByCodeHandler(IDbConnection dbConnection, IMapper mapper)
            {
                _dbConnection = dbConnection;
                _mapper = mapper;
                _compiler = new SqlServerCompiler();
            }

            public async Task<WireColor> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = new SqlKata.Query("WireColor")
                    .Where("Code", request.Code);
                var sqlResult = _compiler.Compile(query);

                var wireColor =
                    await _dbConnection.QuerySingleOrDefaultAsync<POCOs.WireColor>(sqlResult.Sql,
                        new {p0 = sqlResult.Bindings[0]});
                return _mapper.Map<WireColor>(wireColor);
            }
        }
    }
}