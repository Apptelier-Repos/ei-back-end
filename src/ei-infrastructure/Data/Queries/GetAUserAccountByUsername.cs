﻿using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using Dapper;
using ei_core.Entities.UserAccountAggregate;
using MediatR;
using SqlKata.Compilers;

namespace ei_infrastructure.Data.Queries
{
    public class GetAUserAccountByUsername
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<POCOs.UserAccount, UserAccount>();
            }
        }

        /// <summary>
        ///     Query to return a user account when there is a username match, or null otherwise.
        /// </summary>
        public class Query : IRequest<UserAccount>
        {
            public string Username { get; set; }

            public Query(string username)
            {
                Guard.Against.NullOrWhiteSpace(username, nameof(username));
                Username = username;
            }
        }

        public class GetAUserAccountByUsernameHandler : IRequestHandler<Query, UserAccount>
        {
            private readonly SqlServerCompiler _compiler;
            private readonly IDbConnection _dbConnection;
            private readonly IMapper _mapper;

            public GetAUserAccountByUsernameHandler(IDbConnection dbConnection, IMapper mapper)
            {
                _dbConnection = dbConnection;
                _mapper = mapper;
                _compiler = new SqlServerCompiler();
            }

            public async Task<UserAccount> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = new SqlKata.Query("UserAccount")
                    .Where("Username", request.Username);
                var sqlResult = _compiler.Compile(query);

                var userAccountPoco =
                    await _dbConnection.QuerySingleOrDefaultAsync<POCOs.UserAccount>(sqlResult.Sql,
                        new {p0 = sqlResult.Bindings[0]});
                return _mapper.Map<UserAccount>(userAccountPoco);
            }
        }
    }
}