using Individuals.Shared;
using MediatR;

namespace Individuals.Commands.PhoneNumber.DeletePhoneNumber
{
    public class DeletePhoneNumberCommand:IRequest<Result>
    {
        public long? Id { get; set; }
    }
}
