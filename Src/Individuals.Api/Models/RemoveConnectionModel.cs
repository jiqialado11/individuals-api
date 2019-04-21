namespace Individuals.Api.Models
{
    public class RemoveConnectionModel
    {
        public long? ConnectedFromIndividualId { get; set; }
        public long? ConnectedToIndividualId { get; set; }
    }
}
