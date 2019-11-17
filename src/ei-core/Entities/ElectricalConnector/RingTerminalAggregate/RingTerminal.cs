using Humanizer;

namespace ei_core.Entities.ElectricalConnector.RingTerminalAggregate
{
    public sealed class RingTerminal : ElectricalConnector
    {
        public RingTerminal(int id, string model, string imageSource, Cavity cavity) : base(id, model)
        {
            ImageSource = imageSource;
            Cavity = cavity;
        }

        public string ImageSource { get; }

        public Cavity Cavity { get; }

        public override string ToString()
        {
            return $"({Id}) {nameof(RingTerminal).Humanize()} {Model}";
        }
    }
}