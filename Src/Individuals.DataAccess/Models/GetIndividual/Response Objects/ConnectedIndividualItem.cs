using System;

namespace Individuals.DataAccess.Models.GetIndividual.Response_Objects
{
    public class ConnectedIndividualItem
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Gender { get; set; }
        public string PersonalId { get; set; }
        public DateTime? BirthDate { get; set; }
        
        public string ImagePath { get; set; }
        public int? ConnectionType { get; set; }
    }
}
