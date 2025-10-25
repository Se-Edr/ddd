using Application.CQRS.Procedures;
using FluentValidation;

namespace Application.Validators.Procedure
{
    public class UpdateProcedureValidator:AbstractValidator<EditProcedureCommand>
    {
        public UpdateProcedureValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty()
                .WithMessage("Procedure name is required")
                .MinimumLength(3)
                .WithMessage("Procedure name must be at least 3 characters")
                .MaximumLength(100)
                .WithMessage("Procedure name cannot exceed 100 characters");

            RuleFor(x => x.windows)
                .GreaterThan(0)
                .WithMessage("Windows must be greater than 0");

            RuleFor(x => x.price)
                .GreaterThanOrEqualTo(0)
                .When(x => x.price.HasValue)
                .WithMessage("Price cannot be negative");
        }
    }
}
