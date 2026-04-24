using MeuSiteEmMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;   
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeuSiteEmMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            HomeModel home = new HomeModel();

            return View(home);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
