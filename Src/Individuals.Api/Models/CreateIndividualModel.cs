using System;

namespace Individuals.Api.Models
{
    public class CreateIndividualModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public int? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? CityId { get; set; }
    }
}
