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
        private readonly ConciliationService _conciliationService;


        public HomeController(ILogger<HomeController> logger, ConciliationService conciliationService)
        {
            _logger = logger;
            _conciliationService = conciliationService;
        }

        public IActionResult Index()
        {
            return View(new UploadViewModel());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(UploadViewModel uploadViewModel)
        {
            await _conciliationService.Upload(uploadViewModel.Files);
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
