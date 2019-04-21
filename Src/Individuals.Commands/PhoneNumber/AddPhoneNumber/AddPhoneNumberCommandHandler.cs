using System.Threading;
using System.Threading.Tasks;
using Individuals.Decorators;
using Individuals.Decorators.Decorators;
using Individuals.Persistance.Repositories.Contracts;
using Individuals.Shared;
using MediatR;

namespace Individuals.Commands.PhoneNumber.AddPhoneNumber
{
    [BaseDecorator(typeof(ValidationDecorator<,>))]
    [BaseDecorator(typeof(TransactionDecorator<,>))]
    public class AddPhoneNumberCommandHandler:IRequestHandler<AddPhoneNumberCommand,Result>
    {
        private readonly IIndividualsRepository _individualsRepository;

        public AddPhoneNumberCommandHandler(IIndividualsRepository individualsRepository)
        {
            _individualsRepository = individualsRepository;
        }

        public async Task<Result> Handle(AddPhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var individual = await _individualsRepository.FindSingle(request.IndividualId.Value);

            if(individual == null)
                return Result.NotFound("Individual not found with provided identifier");


            foreach (var phoneNumber in request.PhoneNumbers)
            {
                individual.AddPhoneNumber(new Domain.Entities.PhoneNumber(phoneNumber.PhoneNumberType.Value,phoneNumber.PhoneNumber));
            }
            return Result.OK(ResultType.Created);
        }
    }
}
