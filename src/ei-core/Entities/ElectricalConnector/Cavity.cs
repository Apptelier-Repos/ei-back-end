using Ardalis.GuardClauses;

namespace ei_core.Entities.ElectricalConnector
{
    public sealed class Cavity : BaseEntity
    {
        public enum CavityShape
        {
            Ellipse,
            Rectangle
        }

        public Cavity(int id, CavityShape shape, Point position, int height, int width) : base(id)
        {
            Guard.Against.Zero(id, nameof(id));

            Shape = shape;
            Position = position;
            Height = height;
            Width = width;
        }

        public CavityShape Shape { get; }
        public Point Position { get; }
        public int Height { get; }
        public int Width { get; }

        public struct Point // ValueObject
        {
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; }
            public int Y { get; }
        }
    }
}