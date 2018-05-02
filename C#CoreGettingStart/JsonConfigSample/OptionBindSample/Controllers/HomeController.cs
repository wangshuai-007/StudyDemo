using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace OptionBindSample.Controllers
{
    public class HomeController : Controller
    {
        public readonly Class _Class;
        public HomeController(IOptions<Class> clasOptions)
        {
            _Class = clasOptions.Value;
        }
        public IActionResult Index()
        {
            return View(_Class);
        }
    }
}