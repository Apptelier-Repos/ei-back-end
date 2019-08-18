using System.IO;
using System.Threading.Tasks;
using ei_web_api;
using Microsoft.Extensions.Configuration;
using Respawn;

namespace ei_integration_tests
{
    public class SliceFixture
    {
        private static readonly IConfigurationRoot Configuration;
        private static readonly Checkpoint Checkpoint;

        static SliceFixture()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            // TODO: Load DI assemblies here.
            var startup = new Startup(Configuration);
            //var services = new ServiceColl

            Checkpoint = new Checkpoint();
        }

        public static Task ResetCheckpoint() => Checkpoint.Reset(Configuration.GetConnectionString("DefaultConnection"));

    }
}