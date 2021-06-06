using Nibo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Nibo.Domain.Parser
{
    public class OFXParser
    {
        public IEnumerable<Transaction> GetTransactions(IEnumerable<OFXLine> lines)
        {
            var transactions = new List<Transaction>();
            Transaction transaction = null;
            string bankId = null;
            string accountId = null;

            foreach (var line in lines)
            {
                switch (line.TagName)
                {
                    case "BANKID":
                        
                        bankId = line.Value;
                        break;

                    case "ACCTID":

                        accountId = line.Value;
                        break;

                    case "STMTTRN":

                        transaction = new Transaction();
                        break;

                    case "TRNTYPE":

                        transaction.Type = line.Value == "CREDIT" ? 
                            TransactionType.Credit : 
                            TransactionType.Debit;

                        break;

                    case "TRNAMT":

                        transaction.Amount = decimal.Parse(line.Value, CultureInfo.InvariantCulture);
                        break;

                    case "DTPOSTED":
                        
                        if(OFXDateTimeParser.TryParse(line.Value, out DateTimeOffset dateTimeOffset))
                            transaction.DatePosted = dateTimeOffset;

                        break;

                    case "MEMO":

                        transaction.Description = line.Value.Trim();
                        break;

                    case "/STMTTRN":

                        if(transaction != null)
                        {
                            transaction.BankId = bankId;
                            transaction.AccountId = accountId;
                            transactions.Add(transaction);
                            transaction = null;
                        }

                        break;
                }
            }

            return transactions;
        }
    }
}
