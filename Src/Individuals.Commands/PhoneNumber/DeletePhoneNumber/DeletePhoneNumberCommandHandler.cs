using System.Threading;
using System.Threading.Tasks;
using Individuals.Decorators;
using Individuals.Decorators.Decorators;
using Individuals.Persistance.Repositories.Contracts;
using Individuals.Shared;
using MediatR;

namespace Individuals.Commands.PhoneNumber.DeletePhoneNumber
{
    [BaseDecorator(typeof(ValidationDecorator<,>))]
    [BaseDecorator(typeof(TransactionDecorator<,>))]
    public class DeletePhoneNumberCommandHandler:IRequestHandler<DeletePhoneNumberCommand,Result>
    {
        private readonly IPhoneNumbersRepository _phoneNumbersRepository;

        public DeletePhoneNumberCommandHandler(IPhoneNumbersRepository phoneNumbersRepository)
        {
            _phoneNumbersRepository = phoneNumbersRepository;
        }

        public async Task<Result> Handle(DeletePhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var phoneNumber = await _phoneNumbersRepository.FindSingle(request.Id.Value);

            if(phoneNumber == null)
                return Result.NotFound("Couldn't find resource with provided identifier");

            await _phoneNumbersRepository.Delete(request.Id.Value);

            return Result.OK(ResultType.NoContent);

        }
    }
}
