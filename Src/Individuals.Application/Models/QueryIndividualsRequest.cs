using System;
using System.Collections.Generic;

namespace Individuals.Application.Models
{
    public class QueryIndividualsRequest
    {
        public string QueryString { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PersonalId { get; set; }
        public int? Gender { get; set; }
        public string City { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public string OrderBy { get; set; }
    }

    public class QueryIndividualsResponse
    {
        public int TotalCount { get; set; }
        public List<QueryIndividualsItem> List { get; set; }
    }

    public class QueryIndividualsItem
    {
        public int Id { get; set; }
        public string Name { get; }
        public string LastName { get; }
        public string PersonalId { get; }
        public int Gender { get; }
        public string City { get; }
        public DateTime BirthDate { get; }
    }
}
