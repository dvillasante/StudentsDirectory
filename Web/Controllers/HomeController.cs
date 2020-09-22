using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Login(UserLogin userLogin)
        {
            User user = new User();
            user.Username = userLogin.User;
            user.Password = userLogin.Password;
            var result = Business.ApiClientManager.Login(user);

            user.Role = result.Result.Role;
            user.Token = result.Result.Token;
            user.FirstName= result.Result.FirstName;
            user.LastName = result.Result.LastName;
            user.Id = result.Result.Id;
            //return View(result);
            SetToken(user);
            return RedirectToAction("Index", "Student");
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
