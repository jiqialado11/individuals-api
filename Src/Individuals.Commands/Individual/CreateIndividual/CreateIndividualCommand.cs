using System;
using Individuals.Shared;
using MediatR;

namespace Individuals.Commands.Individual.CreateIndividual
{
    public class CreateIndividualCommand:IRequest<Result>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public int? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? CityId { get; set; }
    }
}
