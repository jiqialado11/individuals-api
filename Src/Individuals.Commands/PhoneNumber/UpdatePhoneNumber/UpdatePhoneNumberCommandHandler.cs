using System.Threading;
using System.Threading.Tasks;
using Individuals.Decorators;
using Individuals.Decorators.Decorators;
using Individuals.Persistance.Repositories.Contracts;
using Individuals.Shared;
using MediatR;

namespace Individuals.Commands.PhoneNumber.UpdatePhoneNumber
{
    [BaseDecorator(typeof(ValidationDecorator<,>))]
    [BaseDecorator(typeof(TransactionDecorator<,>))]
    public class UpdatePhoneNumberCommandHandler:IRequestHandler<UpdatePhoneNumberCommand,Result>
    {
        private readonly IPhoneNumbersRepository _phoneNumbersRepository;

        public UpdatePhoneNumberCommandHandler(IPhoneNumbersRepository phoneNumbersRepository)
        {
            _phoneNumbersRepository = phoneNumbersRepository;
        }

        public async Task<Result> Handle(UpdatePhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var phoneNumber = await _phoneNumbersRepository.FindSingle(request.PhoneNumberId.Value);

            if(phoneNumber == null)
                return Result.NotFound("Couldn't find phoneNumber with provided identifier");

            phoneNumber.UpdatePhoneNumber(request.PhoneNumberType.Value,request.PhoneNumber);
            _phoneNumbersRepository.Update(phoneNumber);

            return Result.OK(ResultType.Created);
        }
    }
}
