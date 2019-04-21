using System.Collections.Generic;
using Individuals.Domain.Enums;
using Individuals.Shared;
using MediatR;

namespace Individuals.Commands.PhoneNumber.AddPhoneNumber
{
    public class AddPhoneNumberCommand:IRequest<Result>
    {
        public long? IndividualId { get; set; }
        public List<AddPhoneNumberCommandItem> PhoneNumbers { get; set; }
    }

    public class AddPhoneNumberCommandItem
    {
        public NumberType? PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }
    }
}
