using Microsoft.AspNetCore.Http;
using Nibo.Domain.Interfaces;
using Nibo.Domain.Models;
using Nibo.Domain.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nibo.Web.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _transactionRepository.GetAllAsync();
        }

        public async Task Upload(IEnumerable<IFormFile> formFiles)
        {
            var ofxParser = new OFXParser();
            var existingTransactions = await _transactionRepository.GetAllAsync();

            foreach (var file in formFiles)
            {
                List<OFXLine> ofxLines = await ReadFileAsync(file);
                existingTransactions.AddRange(ofxParser.GetTransactions(ofxLines));
            }

            var newTransactions = existingTransactions.Distinct().Where(x => x.Id == 0).ToList();
            await _transactionRepository.AddRange(newTransactions);
        }

        public static async Task<List<OFXLine>> ReadFileAsync(IFormFile file)
        {
            List<OFXLine> result = new List<OFXLine>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    var lineContent = await reader.ReadLineAsync();
                    if(OFXLine.TryParse(lineContent, out OFXLine ofxLine))
                    {
                        result.Add(ofxLine);
                    }
                }
            }

            return result;
        }
    }
}
