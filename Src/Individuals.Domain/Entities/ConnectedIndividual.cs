using Individuals.Domain.Enums;

namespace Individuals.Domain.Entities
{
    public class ConnectedIndividual
    {
     

        #region Properties

        public Individual ConnectedFromIndividual { get; protected set; }
        public Individual ConnectedToIndividual { get; set; }
        public IndividualsConnectionType ConnectionType { get; protected set; }

        #endregion

        public void SetIndividualsConnection(Individual connectedFrom, Individual connectedTo,
            IndividualsConnectionType connectionType)
        {
            ConnectedFromIndividual = connectedFrom;
            ConnectedToIndividual = connectedTo;
            ConnectionType = connectionType;
        }
    }
}
