using FluentValidation;
using Mediatator.Core.Behs;

namespace Application.Pipeline
{
    public class ValidationBehaviour<TRequest>(IEnumerable<IValidator<TRequest>> validators)
        : IPepePigBehaviour<TRequest>
    {
        public async Task Handle(TRequest request, RequestHandlerDelegate next)
        {
            var context = new ValidationContext<TRequest>(request);
            var results = await Task.WhenAll(validators.Select(v=>v.ValidateAsync(context)));
            var failures = results.Where(r => !r.IsValid).SelectMany(r=>r.Errors);

            if (failures.Any())
                throw new ValidationException(failures);

            await next();
        }
    }
}
