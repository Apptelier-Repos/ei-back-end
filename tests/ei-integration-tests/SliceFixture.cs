using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
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
                catch (Exception e)
                {
                    // The TransactionScope.Complete method commits the transaction. If an exception has been thrown,
                    // Complete is not  called, therefore the transaction is automatically rolled back.

                    // TODO: Log error here.
                    // TODO: Replace re-throw below with a proper exception handling strategy.
                    throw;
                }
            }
        }

        public static Task<T> ExecuteDbContextAsync<T>(Func<IDbConnection, Task<T>> func)
        => ExecuteScopeAsync(sp => func(sp.GetService<IDbConnection>()));
        
        public static Task InsertAsync<TEntity>(TEntity entity) where TEntity : class
        {
            return ExecuteDbContextAsync(db =>
            {
                // TODO: Replace this hard-coded insert with a TEntity-based generator. Allow for insertion of multiple records.
                var sql = "INSERT INTO UserAccount(Username, Password) VALUES (@Username, @Password)";
                return db.ExecuteAsync(sql, new {Username = "ixra", Password = "pass"});
            });
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