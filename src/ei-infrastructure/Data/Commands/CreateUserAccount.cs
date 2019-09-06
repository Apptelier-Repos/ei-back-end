using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using Dapper.Contrib.Extensions;
using ei_core.Exceptions;
using ei_infrastructure.Data.POCOs;
using MediatR;

namespace ei_infrastructure.Data.Commands
{
    public class CreateUserAccount
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, UserAccount>();
            }
        }

        /// <summary>
        ///     Command for creating a new user account.
        /// </summary>
        public class Command : IRequest<int>
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly IDbConnection _dbConnection;
            private readonly IMapper _mapper;

            public CommandHandler(IDbConnection dbConnection, IMapper mapper)
            {
                _dbConnection = dbConnection;
                _mapper = mapper;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                Guard.Against.NullOrWhiteSpace(request.Username, nameof(request.Username));
                Guard.Against.NullOrWhiteSpace(request.Password, nameof(request.Password));

                var userAccount = _mapper.Map<UserAccount>(request);
                try
                {
                    return await _dbConnection.InsertAsync(userAccount);
                }
                catch (SqlException e) when (e.Message.IndexOf("duplicate", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    throw new UsernameAlreadyExistsException(request.Username, e);
                }
            }
        }
    }
}