using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Smarti.Models;
using System.Diagnostics;

namespace Smarti.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(int code)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("Error/404")]
        public IActionResult Error404()
        {
            return View();
        }
}
}