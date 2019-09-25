using System.Drawing;
using Ardalis.GuardClauses;

namespace ei_core.Entities.WireColorAggregate
{
    public sealed class WireColor : BaseEntity
    {
        public string Code { get; }
        public string Name { get; }
        public string TranslatedName { get; }
        public RgbColors Colors { get; }

        public WireColor(int id, string code, string name, string translatedName, RgbColors colors) : base(id)
        {
            Guard.Against.Zero(id, nameof(id));
            Guard.Against.NullOrEmpty(code, nameof(code));
            Guard.Against.NullOrEmpty(translatedName, nameof(translatedName));

            Code = code;
            Name = name;
            TranslatedName = translatedName;
            Colors = colors;
        }
    }
}