using Individuals.Domain.Enums;

namespace Individuals.Domain.Entities
{
    public class PhoneNumber:Entity
    {
        #region Constructors
        protected PhoneNumber()
        {

        }
        public PhoneNumber(NumberType numberType, string number)
        {
            NumberType = numberType;
            Number = number;
        }
        #endregion

        #region Properties

        public NumberType NumberType { get; protected set; }
        public string Number { get; protected set; }

        #endregion

        #region Methods

        public void UpdatePhoneNumber(NumberType type, string phoneNumber)
        {
            NumberType = type;
            Number = phoneNumber;
        }

        #endregion
    }
}
