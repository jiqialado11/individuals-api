using System.Threading;
using System.Threading.Tasks;
using Individuals.Decorators;
using Individuals.Decorators.Decorators;
using Individuals.Domain.Enums;
using Individuals.Persistance.Repositories.Contracts;
using Individuals.Shared;
using MediatR;

namespace Individuals.Commands.Individual.UpdateIndividual
{
    [BaseDecorator(typeof(ValidationDecorator<,>))]
    [BaseDecorator(typeof(TransactionDecorator<,>))]
    public class UpdateIndividualCommandHandler:IRequestHandler<UpdateIndividualCommand,Result>
    {
        private readonly IIndividualsRepository _individualsRepository;
        private readonly ICityRepository _cityRepository;

        public UpdateIndividualCommandHandler(IIndividualsRepository individualsRepository, ICityRepository cityRepository)
        {
            _individualsRepository = individualsRepository;
            _cityRepository = cityRepository;
        }

        public async Task<Result> Handle(UpdateIndividualCommand request, CancellationToken cancellationToken)
        {
            var individual = await _individualsRepository.FindSingle(request.Id.Value);
            
            if(individual == null)
                return Result.NotFound("Couldn't find resource with provided identifier");

            var city = await _cityRepository.FindSingle(request.CityId.Value);
            if(city == null)
                return Result.NotFound($"Couldn't find resource with provided identifier {request.CityId}");

            individual.UpdateIndividual(request.FirstName,request.LastName,(GenderType)request.Gender,request.PersonalNumber,request.BirthDate,city);

            _individualsRepository.Update(individual);
            return Result.OK(ResultType.NoContent);
        }
    }
}
