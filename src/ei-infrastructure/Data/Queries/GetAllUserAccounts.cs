using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dapper.Contrib.Extensions;
using ei_core.Entities.UserAccountAggregate;
using MediatR;

namespace ei_infrastructure.Data.Queries
{
    public class GetAllUserAccounts
    {
        /// <summary>
        ///     Query to return all existing user accounts, or an empty sequence if none exist.
        /// </summary>
        public class Query : IRequest<IEnumerable<UserAccount>>
        {
        }

        public class GetAllUserAccountsHandler : IRequestHandler<Query,
            IEnumerable<UserAccount>>
        {
            private readonly IDbConnection _dbConnection;
            private readonly IMapper _mapper;

            public GetAllUserAccountsHandler(IDbConnection dbConnection, IMapper mapper)
            {
                _dbConnection = dbConnection;
                _mapper = mapper;
            }

            public async Task<IEnumerable<UserAccount>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var userAccountPocos = (await _dbConnection.GetAllAsync<POCOs.UserAccount>()).ToList();
                return _mapper.Map<IEnumerable<UserAccount>>(userAccountPocos);
            }
        }
    }
}