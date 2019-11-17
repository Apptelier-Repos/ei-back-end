using Ardalis.GuardClauses;

namespace ei_core.Entities.ElectricalConnector
{
    public abstract class ElectricalConnector : BaseEntity
    {
        protected ElectricalConnector(int id, string model) : base(id)
        {
            Guard.Against.Zero(id, nameof(id));
            Guard.Against.NullOrEmpty(model, nameof(model));

            Model = model;
        }

        public string Model { get; }
    }
}