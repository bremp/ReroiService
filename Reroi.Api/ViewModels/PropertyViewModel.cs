using Reroi.Api.ViewModels.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Reroi.Api.ViewModels
{
    public class PropertyViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public int Mls { get; set; }
        public double NetOperatingIncome { get; set; }
        public double PurchasePrice { get; set; }
        public double Roi { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new PropertyViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
