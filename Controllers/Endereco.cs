using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeuSiteEmMVC.Controllers
{
    public class Endereco : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
