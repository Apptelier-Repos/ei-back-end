using System.Threading.Tasks;
using ei_slice.POCOs;
using Nito.AsyncEx;

namespace ei_slice
{
    public class Initialize
    {
        private static readonly AsyncLock Mutex;
        private static bool _databaseIsInitialized;
        private static TestSessionDataId _testSessionId;
        private static bool _testDataIsInitialized;

        static Initialize()
        {
            Mutex = new AsyncLock();
        }

        public static async Task DatabaseAsync(TestSessionDataId testSessionId)
        {
            using (await Mutex.LockAsync())
            {
                if (_databaseIsInitialized)
                    return;

                _testSessionId = testSessionId;
                if (await TestDataExistsAsync())
                    await Fixture.ResetCheckpoint();

                _databaseIsInitialized = true;
            }
        }

        public static async Task TestDataAsync()
        {
            using (await Mutex.LockAsync())
            {
                if (_testDataIsInitialized)
                    return;

                if (!await TestDataExistsAsync())
                    await Fixture.InsertTestSessionDataAsync(_testSessionId);

                _testDataIsInitialized = true;
            }
        }

        private static async Task<bool> TestDataExistsAsync()
        {
            var result = await Fixture.FindTestSessionDataAsync(_testSessionId);
            return result != null;
        }
    }
}