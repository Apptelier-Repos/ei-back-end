using System.Drawing;
using Ardalis.GuardClauses;

namespace ei_core.Entities.WireColorAggregate
{
    public sealed class WireColor : BaseEntity
    {
        public WireColor(int id, string code, string name, string translatedName, Color baseColor) : base(id)
        {
            Guard.Against.Zero(id, nameof(id));
            Guard.Against.NullOrEmpty(code, nameof(code));
            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.NullOrEmpty(translatedName, nameof(translatedName));

            Code = code;
            Name = name;
            TranslatedName = translatedName;
            BaseColor = baseColor;
        }

        public WireColor(int id, string code, string name, string translatedName, Color baseColor,
            Color stripeColor) : base(id)
        {
            Guard.Against.Zero(id, nameof(id));
            Guard.Against.NullOrEmpty(code, nameof(code));
            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.NullOrEmpty(translatedName, nameof(translatedName));

            Code = code;
            Name = name;
            TranslatedName = translatedName;
            BaseColor = baseColor;
            StripeColor = stripeColor;
        }

        public string Code { get; }
        public string Name { get; }
        public string TranslatedName { get; }
        public Color BaseColor { get; set; }
        public Color? StripeColor { get; }

        public override string ToString()
        {
            return $"({Id}) {Code}";
        }
    }
}