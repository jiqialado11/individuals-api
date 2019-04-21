using System.Collections.Generic;
using Individuals.Domain.Enums;

namespace Individuals.Api.Models
{
    public class AddPhoneNumbersModel
    {
        public long? IndividualId { get; set; }
        public List<AddPhoneNumber> PhoneNumbers { get; set; }
    }
    public class AddPhoneNumber
    {
      public NumberType? PhoneNumberType { get; set; }
      public string PhoneNumber { get; set; }
    }
}
