using System.ComponentModel.DataAnnotations;

namespace hotel_app.Validation
{
    public class EndDateValidatior:ValidationAttribute
    {
        private readonly string _startDatePropertyName;

        public EndDateValidatior(string startDatePropertyName) {
         _startDatePropertyName  = startDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var startDateProperty = validationContext.ObjectType.GetProperty(_startDatePropertyName);

            if (startDateProperty == null)
            {
                return new ValidationResult($"Unknown property {_startDatePropertyName}");
            }

            var startDateValue = (DateTime)startDateProperty.GetValue(validationContext.ObjectInstance, null);
            var endDateValue = (DateTime)value;

            if (endDateValue <= startDateValue)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
