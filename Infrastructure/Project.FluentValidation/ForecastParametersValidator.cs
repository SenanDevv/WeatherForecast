using FluentValidation;

namespace Project.FluentValidation
{
    public class ForecastParametersValidator : AbstractValidator<ForecastParameters>
    {
        public ForecastParametersValidator()
        {
            RuleFor(x => x.Date)
                .NotNull()
                .WithMessage("Date is required");

            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("City is required");

            RuleFor(x => x.Country)
                .NotEmpty()
                .WithMessage("Country is required");
        }
    }
}
