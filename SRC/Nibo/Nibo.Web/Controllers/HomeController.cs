using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nibo.Web.Models;
using Nibo.Web.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Nibo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TransactionService _transactionService;


        public HomeController(ILogger<HomeController> logger, TransactionService transactionService)
        {
            _logger = logger;
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionService.GetAllAsync();
            return View(transactions.OrderBy(x => x.DatePosted)) ;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IEnumerable<IFormFile> files)
        {
            await _transactionService.Upload(files);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
