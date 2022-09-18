using BlogApi.Dtos;
using FluentValidation;

namespace BlogApi.Validations
{
    public class CreatePostDtoValidator : AbstractValidator<CreatePostDto>
    {
        public CreatePostDtoValidator()
        {
            RuleFor(m => m.Content).NotEmpty();
            RuleFor(m => m.UserId).NotEmpty().GreaterThan(0);
        }
    }
}
