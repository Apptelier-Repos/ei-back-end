using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using Dapper.Contrib.Extensions;
using ei_infrastructure.Data.POCOs;
using MediatR;

namespace ei_infrastructure.Data.Commands
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserAccount, UserAccount>();
        }
    }

    public class CreateUserAccount : IRequest<int>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class CreateUserAccountHandler : IRequestHandler<CreateUserAccount, int>
    {
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;

        public CreateUserAccountHandler(IDbConnection dbConnection, IMapper mapper)
        {
            _dbConnection = dbConnection;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUserAccount request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrWhiteSpace(request.Username, nameof(request.Username));
            Guard.Against.NullOrWhiteSpace(request.Password, nameof(request.Password));

            var userAccount = _mapper.Map<UserAccount>(request);
            return await _dbConnection.InsertAsync(userAccount);
        }
    }
}