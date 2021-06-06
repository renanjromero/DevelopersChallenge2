using Microsoft.AspNetCore.Http;
using Nibo.Domain.Models;
using Nibo.Domain.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nibo.Web.Services
{
    public class ConciliationService
    {
        public async Task Upload(IEnumerable<IFormFile> formFiles)
        {
            var ofxParser = new OFXParser();
            var transactions = new List<Transaction>();

            foreach (var file in formFiles)
            {
                List<OFXLine> ofxLines = await ReadFileAsync(file);
                transactions.AddRange(ofxParser.GetTransactions(ofxLines));
            }

            transactions = transactions.Distinct().ToList();
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
