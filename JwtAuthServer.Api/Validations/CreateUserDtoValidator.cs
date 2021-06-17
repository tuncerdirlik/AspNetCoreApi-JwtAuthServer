using FluentValidation;
using JwtAuthServer.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthServer.Api.Validations
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(k => k.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Email is not valid");
            RuleFor(k => k.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(k => k.UserName).NotEmpty().WithMessage("Username is requried");
        }
    }
}
