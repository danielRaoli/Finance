using Finance.API.Application.Requests;
using Finance.API.Exceptions;
using FluentValidation;

namespace Finance.API.Validators
{
    public class CreateAccountValidator : AbstractValidator<CreateAccountRequest>
    {
        public CreateAccountValidator()
        {
            RuleFor(request => request.UserName).NotEmpty().WithMessage(Resource.NAME_EMPTY);

            RuleFor(request => request).Must(request => request.ConfirmPassword == request.Password).WithMessage(Resource.PASSWORD_MISS_MATCH);
        }
    }
}
