using Nibo.Domain.Types;
using System;

namespace Nibo.Domain.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public int BankId { get; set; }

        public int AccountId { get; set; }

        public TransactionType Type { get; set; }

        public DateTimeOffset DatePosted { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }
    }
}
