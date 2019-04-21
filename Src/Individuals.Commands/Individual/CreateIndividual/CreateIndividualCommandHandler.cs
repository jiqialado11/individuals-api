using System.Threading;
using System.Threading.Tasks;
using Individuals.Decorators;
using Individuals.Decorators.Decorators;
using Individuals.Domain.Enums;
using Individuals.Persistance.Repositories.Contracts;
using Individuals.Shared;
using MediatR;

namespace Individuals.Commands.Individual.CreateIndividual
{
    [BaseDecorator(typeof(ValidationDecorator<,>))]
    [BaseDecorator(typeof(TransactionDecorator<,>))]
    public class CreateIndividualCommandHandler:IRequestHandler<CreateIndividualCommand,Result>
    {
        private readonly IIndividualsRepository _individualsRepository;
        private readonly ICityRepository _cityRepository;

        public CreateIndividualCommandHandler(IIndividualsRepository individualsRepository, ICityRepository cityRepository)
        {
            _individualsRepository = individualsRepository;
            _cityRepository = cityRepository;
        }

        public async Task<Result> Handle(CreateIndividualCommand request, CancellationToken cancellationToken)
        {
            var city = await _cityRepository.FindSingle(request.CityId.Value);
            if (city == null)
                return Result.NotFound("Couldn't find city with providet identifier");

            var individual = new Domain.Entities.Individual(request.FirstName, request.LastName,
                (GenderType) request.Gender.Value, request.PersonalNumber, request.BirthDate.Value, city);

            await _individualsRepository.AddAsync(individual);
            return Result.OK(ResultType.Created);
        }
    }
}
