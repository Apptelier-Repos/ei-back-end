using System;
using Dapper.Contrib.Extensions;

namespace ei_infrastructure.Data.POCOs
{
    public class WireColor
    {
        [Key] public int Id { get; set; }

        [Computed] public DateTime CreationDate { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string TranslatedName { get; set; }
        public string BaseColor { get; set; }
        public string StripeColor { get; set; }
    }
}