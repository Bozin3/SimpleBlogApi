using AutoMapper;
using BlogApi.Data.Entities;
using BlogApi.Data.Repository;
using BlogApi.Dtos;
using BlogApi.Utils;
using FluentValidation;

namespace BlogApi.Endpoints
{
    public static class AuthEndpoints
    {
        public static WebApplication MapAuthEndpoints(this WebApplication app)
        {
            app.MapPost("/api/auth", async (
                IUserRepository userRepository, 
                IMapper mapper,
                IConfiguration configuration,
                IValidator <AuthDto> validator, 
                AuthDto authDto) =>
            {
                var validationResult = validator.Validate(authDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(x => x.ErrorMessage);
                    return Results.BadRequest(new { errors = errors });
                }

                var user = await userRepository.GetUserByUsername(authDto.Username);

                if (user is null)
                {
                    return Results.Unauthorized();
                }

                if (!PasswordValidator.CheckValidPassword(authDto.Password, user))
                {
                    return Results.Unauthorized();
                }

                var tokenDto = new TokenDto
                {
                    Token = TokenBuilder.CreateToken(user, configuration["Token"]),
                    User = mapper.Map<ReadUserDto>(user)
                };

                return Results.Ok(tokenDto);
            });

            app.MapPost("/api/register", async (
                    IUserRepository userRepository,
                    IMapper mapper,
                    IValidator <RegisterUserDto> validator, 
                    RegisterUserDto registerUserDto
                ) => 
            {
                var validationResult = validator.Validate(registerUserDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(x => x.ErrorMessage);
                    return Results.BadRequest(new { errors = errors });
                }

                byte[] passwordHash, passwordSalt;
                PasswordValidator.CreatePasswordHash(registerUserDto.Password, out passwordHash, out passwordSalt);

                var user = mapper.Map<User>(registerUserDto);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await userRepository.RegisterUser(user!);
                return Results.Ok();
            });

            return app;
        }
    }
}
