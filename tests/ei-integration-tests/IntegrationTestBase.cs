﻿using System.Threading.Tasks;
using Nito.AsyncEx;
using Xunit;

namespace ei_integration_tests
{
    public abstract class IntegrationTestBase : IAsyncLifetime
    {
        private static readonly AsyncLock Mutex = new AsyncLock();

        private static bool _initialized;

        public virtual async Task InitializeAsync()
        {
            if (_initialized)
                return;

            using (await Mutex.LockAsync())
            {
                if (_initialized)
                    return;

                await SliceFixture.ResetCheckpoint();

                _initialized = true;
            }
        }

        public virtual Task DisposeAsync() => Task.CompletedTask;
    }
}