using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using ei_infrastructure.Data.POCOs;

// ReSharper disable StringLiteralTypo

namespace ei_infrastructure.Data
{
    public static class DbInitializer
    {
        private static bool _settingsHaveBeenInitialized;

        public static void InitializeSettings()
        {
            if (_settingsHaveBeenInitialized) return;
            SqlMapperExtensions.TableNameMapper = type => type.Name; // https://dapper-tutorial.net/knowledge-base/32204808/dapper-use-singular-table-name
            _settingsHaveBeenInitialized = true;
        }

        public static async Task SeedAsync(IDbConnection dbConnection)
        {
            if (!dbConnection.GetAll<UserAccount>().Any())
                await dbConnection.InsertAsync(GetPreconfiguredUserAccounts());
        }

        private static IEnumerable<UserAccount> GetPreconfiguredUserAccounts()
        {
            return new[]
            {
                new UserAccount {Username = "limon", Password = "limonxz123"},
                new UserAccount {Username = "rios", Password = "loerardo123"},
                new UserAccount {Username = "mota", Password = "maciaseo123"},
                new UserAccount {Username = "lesair", Password = "israel123"}
            };
        }
    }
}