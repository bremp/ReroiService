using FluentValidation;

namespace Reroi.Api.ViewModels.Validations
{
    public class CountryViewModelValidator : AbstractValidator<CountryViewModel>
    {
        public CountryViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(x => x.EpiIndex).NotEmpty().WithMessage("Epi Index cannot be empty");
        }
    }
}
