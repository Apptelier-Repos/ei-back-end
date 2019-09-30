using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dapper.Contrib.Extensions;
using ei_core.Entities.WireColorAggregate;
using MediatR;

namespace ei_infrastructure.Data.Queries
{
    public class GetAllWireColors
    {
        /// <summary>
        ///     Query to return all existing wire colors, or an empty sequence if none exist.
        /// </summary>
        public class Query : IRequest<IEnumerable<WireColor>>
        {
        }

        public class GetAllWireColorsHandler : IRequestHandler<Query, IEnumerable<WireColor>>
        {
            private readonly IDbConnection _dbConnection;
            private readonly IMapper _mapper;

            public GetAllWireColorsHandler(IDbConnection dbConnection, IMapper mapper)
            {
                _dbConnection = dbConnection;
                _mapper = mapper;
            }

            public async Task<IEnumerable<WireColor>> Handle(Query request, CancellationToken cancellationToken)
            {
                var wireColorPocos = (await _dbConnection.GetAllAsync<POCOs.WireColor>()).ToList();
                return _mapper.Map<IEnumerable<WireColor>>(wireColorPocos);
            }
        }
    }
}