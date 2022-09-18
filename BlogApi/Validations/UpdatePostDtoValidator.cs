using BlogApi.Dtos;
using FluentValidation;

namespace BlogApi.Validations
{
    public class UpdatePostDtoValidator : AbstractValidator<UpdatePostDto>
    {
        public UpdatePostDtoValidator()
        {
            RuleFor(m => m.Id).NotEmpty().GreaterThan(0); ;
            RuleFor(m => m.Content).NotEmpty();
        }
    }
}
