﻿using Finance.API.Domain.Enums;

namespace Finance.API.Domain.Entities
{
    public class Transaction(Guid userId, decimal value, TransactionType transactionType, string description)
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; } = userId;
        public string Description { get; set; } = description;
        public decimal Value { get; set; } = value;
        public TransactionType TransactionType { get; set; } = transactionType;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
