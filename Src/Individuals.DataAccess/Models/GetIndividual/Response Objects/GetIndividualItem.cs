using System;
using System.Collections.Generic;

namespace Individuals.DataAccess.Models.GetIndividual.Response_Objects
{
    public class GetIndividualItem
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Gender { get; set; }
        public string PersonalId { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? CityId { get; set; }
        public string City { get; set; }
        public string ImagePath { get; set; }
        public List<PhoneNumberItem> PhoneNumbers { get; set; }
        public List<ConnectedIndividualItem> ConnectedIndividuals { get; set; }
    }
}
