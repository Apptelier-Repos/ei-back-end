using System.Drawing;

namespace ei_core.Entities.WireColorAggregate
{
    public struct RgbColors
    {
        public Color[] Colors { get; }

        public RgbColors(Color color1) : this()
        {
            Colors = new Color[1];
            Colors[0] = color1;
        }

        public RgbColors(Color color1, Color color2)
        {
            Colors = new Color[2];
            Colors[0] = color1;
            Colors[1] = color2;
        }
    }
}