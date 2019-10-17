using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TextRedactor.Controllers
{
    public class RedactorController : Controller
    {
        [HttpGet]
        public IActionResult TextRedact()
        {
            return View();
        }
    }
}