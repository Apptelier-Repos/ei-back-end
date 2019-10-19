using System.Threading.Tasks;
using ei_slice;
using ei_slice.POCOs;
using Xunit;

namespace ei_integration_tests
{
    public abstract class IntegrationTestBase : IAsyncLifetime
    {
        public virtual async Task InitializeAsync()
        {
            await Initialize.DatabaseAsync(TestSessionDataId.IntegrationTests);
        }

        public virtual Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}