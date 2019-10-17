using System.Threading.Tasks;
using ei_slice;
using Xunit;

namespace ei_integration_tests
{
    public abstract class IntegrationTestBase : IAsyncLifetime
    {
        public virtual async Task InitializeAsync()
        {
            await Initialize.DatabaseAsync();
        }

        public virtual Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}