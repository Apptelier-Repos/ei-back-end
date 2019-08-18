using ei_infrastructure.Data.Queries;
using Xunit;

namespace ei_integration_tests.Features.UserAccount
{
    public class UserAccountQueryShould : IntegrationTestBase
    {
        [Fact]
        public void ReturnAUserAccountBasedOnAUsername()
        {
            // TODO: Gather user accounts test data here.
            // TODO: Asynchronously persist the user accounts test data into the persistence media (e.g. database).
            // TODO: Temporarily using username const below. Remove it after this is gathered from the persistence media.
            // ReSharper disable once StringLiteralTypo
            const string username = "lesair";

            var query = new UserAccountQuery {Username = username};
        }
    }
}
