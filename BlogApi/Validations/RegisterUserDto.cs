using BlogApi.Dtos;
using FluentValidation;

namespace BlogApi.Validations
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(m => m.Username).NotEmpty();
            RuleFor(m => m.Password).NotEmpty();
        }
    }
}
