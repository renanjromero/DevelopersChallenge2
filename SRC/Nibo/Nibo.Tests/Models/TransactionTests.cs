using FluentAssertions;
using Nibo.Domain.Models;
using System;
using Xunit;

namespace Nibo.Tests.Models
{
    public class TransactionTests
    {
        [Fact]
        public void Comparing_equal_transactions()
        {
            Transaction transaction1 = new Transaction(
                bankId: "0341",
                accountId: "7037300576",
                type: TransactionType.Credit, 
                datePosted: new DateTimeOffset(year: 2021, month: 6, day: 3, hour: 18, minute: 8, second: 0, TimeSpan.Zero),
                amount: 153.10m,
                description: "DOC INT 516011 microlux"
            );

            Transaction transaction2 = new Transaction(
                bankId: "0341",
                accountId: "7037300576",
                type: TransactionType.Credit,
                datePosted: new DateTimeOffset(year: 2021, month: 6, day: 3, hour: 18, minute: 8, second: 0, TimeSpan.Zero),
                amount: 153.10m,
                description: "DOC INT 516011 microlux"
            );

            bool result = transaction1.Equals(transaction2);

            result.Should().BeTrue();
        }

        [Fact]
        public void Comparing_different_transactions()
        {
            Transaction transaction1 = new Transaction(
                bankId: "0341",
                accountId: "7037300576",
                type: TransactionType.Debit,
                datePosted: new DateTimeOffset(year: 2021, month: 6, day: 3, hour: 18, minute: 8, second: 0, TimeSpan.Zero),
                amount: 153.10m,
                description: "DOC INT 516011 microlux"
            );

            Transaction transaction2 = new Transaction(
                bankId: "0341",
                accountId: "7037300575",
                type: TransactionType.Debit,
                datePosted: new DateTimeOffset(year: 2021, month: 6, day: 3, hour: 18, minute: 8, second: 1, TimeSpan.Zero),
                amount: 153.11m,
                description: "DOC INT 516011 microluxx"
            );

            bool result = transaction1.Equals(transaction2);

            result.Should().BeFalse();
        }
    }
}
