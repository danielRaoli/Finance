using Finance.API.Domain.Entities;
using Finance.API.Domain.Enums;
using Finance.API.Exceptions;
using Finance.API.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Finance.API.Application.Requests
{
    public class AddTransactionRequest
    {
        

        public Guid UserId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }



        public Transaction ToEntity() => new(this.UserId, this.Value, this.Type,this.Date, this.Description);


        public void Validate(IValidator<AddTransactionRequest> validator)
        {
            var result = validator.Validate(this);

            if (result.IsValid is false)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new OnValidateException(errors);
            }
        }
    }
}
