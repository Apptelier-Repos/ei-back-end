namespace ei_slice.POCOs
{
    /// <summary>
    /// Test session data identifier. Can be either data for integration tests or functional tests.
    /// </summary>
    public enum TestSessionDataId
    {
        /// <summary>
        ///     Integration tests data.
        /// </summary>
        IntegrationTests = 1,

        /// <summary>
        ///     Functional tests data.
        /// </summary>
        FunctionalTests = 2
    }
}