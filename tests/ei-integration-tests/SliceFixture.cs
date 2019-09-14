using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;
using Dapper.Contrib.Extensions;
using ei_web_api;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;

namespace ei_integration_tests
{
    public class SliceFixture
    {
        private static readonly IConfigurationRoot Configuration;
        private static readonly Checkpoint Checkpoint;
        private static readonly IServiceScopeFactory ScopeFactory;

        static SliceFixture()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            var startup = new Startup(Configuration);
            var services = new ServiceCollection();
            startup.ConfigureServices(services);
            var provider = services.BuildServiceProvider();
            ScopeFactory = provider.GetService<IServiceScopeFactory>();
            Checkpoint = new Checkpoint();
            ei_infrastructure.Data.DbInitializer.InitializeSettings();
        }

        public static Task ResetCheckpoint() => Checkpoint.Reset(Configuration.GetConnectionString("DefaultConnection"));

        public static async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> func)
        {
            using (var serviceScope = ScopeFactory.CreateScope())
            {
                try
                {
                    T result;
                    using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        using (var db = serviceScope.ServiceProvider.GetService<IDbConnection>())
                        {
                            result = await func(serviceScope.ServiceProvider).ConfigureAwait(false);
                        }
                        transactionScope.Complete();
                    }

                    return result;
                }
                catch (Exception)
                {
                    // The TransactionScope.Complete method commits the transaction. If an exception has been thrown,
                    // Complete is not  called, therefore the transaction is automatically rolled back.

                    // TODO: Add a logging entry here with feature #164 (https://dev.azure.com/Apptelier/Entrenamiento%20Imaginativo/_workitems/edit/164).
                    // TODO: Replace re-throw below with a proper exception handling strategy.
                    throw;
                }
            }
        }

        public static Task<T> ExecuteDbContextAsync<T>(Func<IDbConnection, Task<T>> func)
        => ExecuteScopeAsync(sp => func(sp.GetService<IDbConnection>()));
        
        public static Task InsertAsync<TEntity>(TEntity entity) where TEntity : class
        {
            return ExecuteDbContextAsync(db => db.InsertAsync(entity));
        }

        public static Task<T> FindAsync<T>(int id) where T : class
        {
            return ExecuteDbContextAsync(db => db.GetAsync<T>(id));
        }

        public static Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            return ExecuteScopeAsync(sp =>
            {
                var mediator = sp.GetService<IMediator>();

                return mediator.Send(request);
            });
        }
    }
}