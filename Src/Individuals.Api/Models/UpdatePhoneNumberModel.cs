using Individuals.Domain.Enums;

namespace Individuals.Api.Models
{
    public class UpdatePhoneNumberModel
    {
        public long? PhoneNumberId { get; set; }
        public NumberType? PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }
    }
}
