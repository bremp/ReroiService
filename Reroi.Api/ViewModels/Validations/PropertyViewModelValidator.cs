using FluentValidation;

namespace Reroi.Api.ViewModels.Validations
{
    public class PropertyViewModelValidator : AbstractValidator<PropertyViewModel>
    {
        public PropertyViewModelValidator()
        {
            RuleFor(x => x.Mls).NotEmpty().WithMessage("MLS cannot be empty");
            RuleFor(x => x.PurchasePrice).NotEmpty().WithMessage("Purchase price cannot be empty");
        }
    }
}
