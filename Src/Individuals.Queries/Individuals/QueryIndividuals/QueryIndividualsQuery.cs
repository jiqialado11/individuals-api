using System;
using Individuals.Shared;
using MediatR;

namespace Individuals.Queries.Individuals.QueryIndividuals
{
    public class QueryIndividualsQuery:IRequest<Result>
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
}
