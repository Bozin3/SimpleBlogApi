using BlogApi.Dtos;
using FluentValidation;

namespace BlogApi.Validations
{
    public class AuthDtoValidator : AbstractValidator<AuthDto>
    {
        public AuthDtoValidator()
        {
            RuleFor(m => m.Username).NotEmpty();
            RuleFor(m => m.Password).NotEmpty();
        }
    }
}
