using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Features.Auth.Register
{
    public class Validator
    {

        public class RegisterUserDtoValidator : AbstractValidator<RegisterRequest>
        {
            public RegisterUserDtoValidator()
            {
                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email required")
                    .EmailAddress();

                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MinimumLength(3);
            }
        }
    }
}
