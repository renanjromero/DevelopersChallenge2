using FluentAssertions;
using Nibo.Domain.Models;
using Nibo.Domain.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Nibo.Tests.Parser
{
    public class OFXParserTests
    {
        [Fact]
        public void Parsing_empty_file()
        {
            OFXLine[] lines = new OFXLine[]{};
            var ofxParser = new OFXParser();

            IEnumerable<Transaction> transactions = ofxParser.GetTransactions(lines);

            transactions.Should().BeEmpty();
        }

        [Fact]
        public void Parsing_a_debit_transaction()
        {
            //Arrange

            OFXLine[] lines = new OFXLine[]
            {
                new OFXLine(tagName: "BANKID", value: "0341"),
                new OFXLine(tagName: "ACCTID", value: "7037300576"),
                new OFXLine(tagName: "STMTTRN"),
                new OFXLine(tagName: "TRNTYPE", value:"DEBIT"),
                new OFXLine(tagName: "DTPOSTED", value: "20140211100000[-03:EST]"),
                new OFXLine(tagName: "TRNAMT", value: "-140.00"),
                new OFXLine(tagName: "MEMO", value: "SAQUE 24H 12725743"),
                new OFXLine(tagName: "/STMTTRN")
            };

            var ofxParser = new OFXParser();    

            //Act

            IEnumerable<Transaction> transactions = ofxParser.GetTransactions(lines);

            //Assert

            transactions.Should().HaveCount(1);
            transactions.First().BankId.Should().Be("0341");
            transactions.First().AccountId.Should().Be("7037300576");
            transactions.First().Type.Should().Be(TransactionType.Debit);
            transactions.First().DatePosted.Should().Be(new DateTimeOffset(year: 2014, month: 02, day: 11, hour: 10, minute: 00, second: 00, offset: new TimeSpan(-8, 0, 0)));
            transactions.First().Amount.Should().Be((decimal)-140.00);
            transactions.First().Description.Should().Be("SAQUE 24H 12725743");
        }

        [Fact]
        public void Parsing_a_credit_transaction()
        {
            //Arrange

            OFXLine[] lines = new OFXLine[]
            {
                new OFXLine(tagName: "BANKID", value: "0341"),
                new OFXLine(tagName: "ACCTID", value: "7037300576"),
                new OFXLine(tagName: "STMTTRN"),
                new OFXLine(tagName: "TRNTYPE", value:"CREDIT"),
                new OFXLine(tagName: "DTPOSTED", value: "20140217100000[-03:EST]"),
                new OFXLine(tagName: "TRNAMT", value: "1556.91"),
                new OFXLine(tagName: "MEMO", value: "INT RESGATE   SPECIAL RF"),
                new OFXLine(tagName: "/STMTTRN")
            };

            var ofxParser = new OFXParser();

            //Act

            IEnumerable<Transaction> transactions = ofxParser.GetTransactions(lines);

            //Assert

            transactions.Should().HaveCount(1);
            transactions.First().BankId.Should().Be("0341");
            transactions.First().AccountId.Should().Be("7037300576");
            transactions.First().Type.Should().Be(TransactionType.Credit);
            transactions.First().DatePosted.Should().Be(new DateTimeOffset(year: 2014, month: 02, day: 17, hour: 10, minute: 00, second: 00, offset: new TimeSpan(-8, 0, 0)));
            transactions.First().Amount.Should().Be((decimal)1556.91);
            transactions.First().Description.Should().Be("INT RESGATE   SPECIAL RF");
        }

        [Fact]
        public void Parsing_multiple_transactions()
        {
            //Arrange

            OFXLine[] lines = new OFXLine[]
            {
                new OFXLine(tagName: "STMTTRN"),
                new OFXLine(tagName: "TRNTYPE", value:"DEBIT"),
                new OFXLine(tagName: "DTPOSTED", value: "20140219100000[-03:EST]"),
                new OFXLine(tagName: "TRNAMT", value: "-500.00"),
                new OFXLine(tagName: "MEMO", value: "TBI 8123.05928-2ana"),
                new OFXLine(tagName: "/STMTTRN"),
                new OFXLine(tagName: "STMTTRN"),
                new OFXLine(tagName: "TRNTYPE", value:"DEBIT"),
                new OFXLine(tagName: "DTPOSTED", value: "20140219100000[-03:EST]"),
                new OFXLine(tagName: "TRNAMT", value: "-345.00"),
                new OFXLine(tagName: "MEMO", value: "CH COMPENSADO 399 000324"),
                new OFXLine(tagName: "/STMTTRN"),
                new OFXLine(tagName: "STMTTRN"),
                new OFXLine(tagName: "TRNTYPE", value:"CREDIT"),
                new OFXLine(tagName: "DTPOSTED", value: "20140219100000[-03:EST]"),
                new OFXLine(tagName: "TRNAMT", value: "3001.06"),
                new OFXLine(tagName: "MEMO", value: "INT RESGATE   SPECIAL RF"),
                new OFXLine(tagName: "/STMTTRN")
            };

            var ofxParser = new OFXParser();

            //Act

            IEnumerable<Transaction> transactions = ofxParser.GetTransactions(lines);

            //Assert

            transactions.Should().HaveCount(3);
        }
    }
}
