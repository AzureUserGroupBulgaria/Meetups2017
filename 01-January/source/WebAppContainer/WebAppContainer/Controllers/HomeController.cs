using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAppContainer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            this.ViewData["OS"] = System.Runtime.InteropServices.RuntimeInformation.OSDescription;

            return View();
        }
    }
}
