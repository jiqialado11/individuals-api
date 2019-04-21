using Individuals.Domain.Enums;
using Individuals.Shared;
using MediatR;

namespace Individuals.Commands.PhoneNumber.UpdatePhoneNumber
{
    public class UpdatePhoneNumberCommand:IRequest<Result>
    {
        public long? PhoneNumberId { get; set; }
        public NumberType? PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }
    }
}

