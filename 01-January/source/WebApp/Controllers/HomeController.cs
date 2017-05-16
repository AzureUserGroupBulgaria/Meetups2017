using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Configuration;
using Microsoft.Extensions.Options;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ConnectionStringSettings connectionStrings;

        public HomeController(IOptions<ConnectionStringSettings> options)
        {
            this.connectionStrings = options.Value;
        }

        public IActionResult Index()
        {
            this.ViewData["ConnectionString"] = this.connectionStrings.DefaultConnection;

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
