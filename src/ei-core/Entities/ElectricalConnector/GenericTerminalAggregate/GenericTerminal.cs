using Humanizer;

namespace ei_core.Entities.ElectricalConnector.GenericTerminalAggregate
{
    public sealed class GenericTerminal : ElectricalConnector
    {
        public GenericTerminal(int id, string model, Cavity cavity) : base(id, model)
        {
            Cavity = cavity;
        }

        public Cavity Cavity { get; }

        public override string ToString()
        {
            return $"({Id}) {nameof(GenericTerminal).Humanize()} {Model}";
        }
    }
}