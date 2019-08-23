using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ei_infrastructure.Data.Queries
{
    public class DemoQuery : IRequest<DemoResponse>
    {
        public int Number { get; set; }
    }

    public class DemoResponse
    {
        public string NumberAsString { get; set; }
    }

    public class DemoQueryHandler : IRequestHandler<DemoQuery, DemoResponse>
    {
        public async Task<DemoResponse> Handle(DemoQuery request, CancellationToken cancellationToken)
        {
            return await new Task<DemoResponse>(() => new DemoResponse { NumberAsString = request.Number.ToString() });
        }
    }
}