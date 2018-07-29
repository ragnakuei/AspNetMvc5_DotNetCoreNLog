using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetCoreNLog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> _logger)
        {
            this._logger = _logger;
        }

        // GET: Home
        public ActionResult Index()
        {
            _logger.LogDebug("Test");
            return View();
        }
    }
}