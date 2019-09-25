using System.Drawing;

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
            Code = code;
            Name = name;
            TranslatedName = translatedName;
            Colors = colors;
        }
    }
}