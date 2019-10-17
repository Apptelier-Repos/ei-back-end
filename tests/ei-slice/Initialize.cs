using System.Threading.Tasks;
using Nito.AsyncEx;

namespace ei_slice
{
    public class Initialize
    {
        private static readonly AsyncLock Mutex;
        private static bool _initialized;

        static Initialize()
        {
            Mutex = new AsyncLock();
        }

        public static async Task DatabaseAsync()
        {
            using (await Mutex.LockAsync())
            {
                if (_initialized)
                    return;

                await Fixture.ResetCheckpoint();

                _initialized = true;
            }
        }
    }
}