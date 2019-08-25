using System.Data;
using System.Threading;
using System.Threading.Tasks;
using UserAccountAggregate = ei_core.Entities.UserAccountAggregate;
using AutoMapper;
using Dapper;
using MediatR;
using SqlKata;
using SqlKata.Compilers;

namespace ei_infrastructure.Data.Queries
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<POCOs.UserAccount, UserAccountAggregate.UserAccount>();
        }
    }

    public class UserAccountQuery : IRequest<UserAccountAggregate.UserAccount>
    {
        public string Username { get; set; }
    }

    public class UserAccountQueryHandler : IRequestHandler<UserAccountQuery, UserAccountAggregate.UserAccount>
    {
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;
        private readonly SqlServerCompiler _compiler;

        public UserAccountQueryHandler(IDbConnection dbConnection, IMapper mapper)
        {
            _dbConnection = dbConnection;
            _mapper = mapper;
            _compiler = new SqlServerCompiler();
        }

        public async Task<UserAccountAggregate.UserAccount> Handle(UserAccountQuery request, CancellationToken cancellationToken)
        {
            var query = new Query("UserAccount")
                .Where("Username", request.Username);
            var sqlResult = _compiler.Compile(query);

            var userAccountDto =
                await _dbConnection.QuerySingleOrDefaultAsync<UserAccountAggregate.UserAccount>(sqlResult.Sql,
                    new {p0 = sqlResult.Bindings[0]});
            return _mapper.Map<UserAccountAggregate.UserAccount>(userAccountDto);
        }
    }
}