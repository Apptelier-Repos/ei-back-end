using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using ei_core.Entities.UserAccountAggregate;
using ei_infrastructure.Data.Queries.DTOs;
using MediatR;
using SqlKata;
using SqlKata.Compilers;

namespace ei_infrastructure.Data.Queries
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserAccountDto, UserAccount>();
        }
    }

    public class UserAccountQuery : IRequest<UserAccount>
    {
        public string Username { get; set; }
    }

    public class UserAccountQueryHandler : IRequestHandler<UserAccountQuery, UserAccount>
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

        public async Task<UserAccount> Handle(UserAccountQuery request, CancellationToken cancellationToken)
        {
            var query = new Query("UserAccountDto")
                .Where("Username", request.Username);
            var sqlResult = _compiler.Compile(query);
            try
            {
                UserAccount userAccount;
                using (_dbConnection)
                {
                    var userAccountDto = await _dbConnection.QuerySingleAsync<UserAccountDto>(sqlResult.Sql, new { p0 = sqlResult.Bindings[0] });
                    userAccount = _mapper.Map<UserAccount>(userAccountDto);
                }

                return userAccount;
            }
            catch (InvalidOperationException e) when (e.Message.IndexOf("no elements", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return null;
            }
        }
    }
}