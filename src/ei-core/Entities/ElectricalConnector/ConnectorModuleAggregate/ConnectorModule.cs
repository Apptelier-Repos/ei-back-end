using System.Collections.Generic;
using Humanizer;

namespace ei_core.Entities.ElectricalConnector.ConnectorModuleAggregate
{
    public class ConnectorModule : ElectricalConnector
    {
        public ConnectorModule(int id, string model, string code, string imageSource, IEnumerable<Cavity> cavities) :
            base(id, model)
        {
            Code = code;
            ImageSource = imageSource;
            Cavities = cavities;
        }

        public string Code { get; set; }

        public string ImageSource { get; }

        public IEnumerable<Cavity> Cavities { get; }

        public override string ToString()
        {
            return $"({Id}) {nameof(ConnectorModule).Humanize()} {Model}";
        }
    }
}