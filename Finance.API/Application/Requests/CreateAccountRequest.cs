using Finance.API.Exceptions;
using Finance.API.Validators;
using FluentValidation;

namespace Finance.API.Application.Requests
{
    public class CreateAccountRequest 
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }


        public void Validate(IValidator<CreateAccountRequest> validator)
        {
            var result = validator.Validate(this);

            if(result.IsValid is false)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new OnValidateException(errors);
            }
        }
    }
}
