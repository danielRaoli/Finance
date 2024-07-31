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
            RuleFor(request => request.Password).
                 MinimumLength(6).WithMessage(Resource.INVALID_PASSWORD)
                .Matches("[A-Z]").WithMessage(Resource.INVALID_PASSWORD)
                .Matches("[a-z]").WithMessage(Resource.INVALID_PASSWORD)
                .Matches("0-9").WithMessage(Resource.INVALID_PASSWORD)
                .Matches("[^a-zA-Z0-9]").WithMessage(Resource.INVALID_PASSWORD);

            RuleFor(request => request).Must(request => request.ConfirmPassword == request.Password).WithMessage(Resource.PASSWORD_MISS_MATCH);
        }
    }
}
