using Finance.API.Application.Requests;
using Finance.API.Exceptions;
using FluentValidation;

namespace Finance.API.Validators
{
    public class AddTransactionValidator : AbstractValidator<AddTransactionRequest>
    {
        public AddTransactionValidator()
        {
            RuleFor(request => request.Type).IsInEnum().WithMessage(Resource.TYPE_ENUM_ERROR);

            RuleFor(request => request.Description).NotEmpty().WithMessage(Resource.DESCRIPTION_EMPTY)
                .Length(5, 40).WithMessage(Resource.DESCRIPTION_LENTGHT_ERROR);
        }
    }
}
