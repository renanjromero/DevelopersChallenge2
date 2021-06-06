using System;
using System.Diagnostics.CodeAnalysis;

namespace Nibo.Domain.Models
{
    public class Transaction: IEquatable<Transaction>
    {
        public Transaction()
        {
        }

        public Transaction(string bankId, string accountId, TransactionType type, DateTimeOffset datePosted, decimal amount, string description)
        {
            BankId = bankId;
            AccountId = accountId;
            Type = type;
            DatePosted = datePosted;
            Amount = amount;
            Description = description;
        }

        public int Id { get; set; }

        public string BankId { get; set; }

        public string AccountId { get; set; }

        public TransactionType Type { get; set; }

        public DateTimeOffset DatePosted { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public bool Equals([AllowNull] Transaction other)
        {
            return 
                other != null &&
                other.BankId.Equals(BankId) &&
                other.AccountId.Equals(AccountId) &&
                other.Type.Equals(Type) &&
                other.DatePosted.Equals(DatePosted) &&
                other.Amount.Equals(Amount) &&
                other.Description.Equals(Description);
        }

        public override int GetHashCode()
        {
            return BankId.GetHashCode() ^ AccountId.GetHashCode() ^ Type.GetHashCode() ^ DatePosted.GetHashCode() ^ Amount.GetHashCode() ^ Description.GetHashCode();
        }
    }
}
