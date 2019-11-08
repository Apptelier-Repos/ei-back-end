using System;
using Dapper.Contrib.Extensions;

namespace ei_slice.POCOs
{
    internal class TestSessionData
    {
        [ExplicitKey] public TestSessionDataId Id { get; set; }

        [Computed] public DateTime CreationDate { get; set; }
    }
}