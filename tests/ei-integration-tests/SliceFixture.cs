using Respawn;

namespace ei_integration_tests
{
    public class SliceFixture
    {
        private static readonly Checkpoint _checkpoint;

        static SliceFixture()
        {
            _checkpoint = new Checkpoint();
        }
    }
}