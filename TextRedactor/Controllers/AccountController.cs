using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TextRedactor.Data.Models;
using TextRedactor.Data.Repositories;
using TextRedactor.Models;

namespace TextRedactor.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepository userRepository;
        public AccountController(BaseRepository<User> _userRepository)
        {
            userRepository = (UserRepository)_userRepository;
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(AuthorizeViewModel model)
        {
            if (!userRepository.CheckUserSignIn(model.Name, model.Password))
            {
                ModelState.AddModelError(string.Empty, "Invalid name or pasword");
                return RedirectToAction("SignUp");
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(AuthorizeViewModel model)
        {
            if (model.Password == model.RepeatePassword)
            {
                userRepository.Create(model.Name, model.Password);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}