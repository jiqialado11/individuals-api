using System;
using Individuals.Shared;
using MediatR;

namespace Individuals.Commands.Individual.UpdateIndividual
{
    public class UpdateIndividualCommand:IRequest<Result>
    {
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public int? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public long? CityId { get; set; }
    }
}
