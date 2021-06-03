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
        public IEnumerable<Transaction> GetTransactions(OFXLine[] lines)
        {
            var transactions = new List<Transaction>();
            Transaction transaction = null;

            foreach (var line in lines)
            {
                switch (line.TagName)
                {
                    case "STMTTRN":

                        transaction = new Transaction();
                        break;

                    case "TRNTYPE":

                        transaction.Type = line.Value == "CREDIT" ? 
                            Types.TransactionType.Credit : 
                            Types.TransactionType.Debit;

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
