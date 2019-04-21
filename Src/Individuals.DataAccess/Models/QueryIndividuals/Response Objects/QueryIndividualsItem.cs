using System;

namespace Individuals.DataAccess.Models.QueryIndividuals
{
    public class QueryIndividualsItem
    {
        public int Id { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public string PersonalId { get; }
        public int Gender { get; }
        public string City { get; }
        public DateTime BirthDate { get; }
    }
}
