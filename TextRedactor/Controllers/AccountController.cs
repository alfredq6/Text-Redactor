using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
        public IActionResult SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!userRepository.CheckUserSignIn(model.Name, model.Password))
                    ModelState.AddModelError(string.Empty, "Invalid name or password");
                else
                    return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Password == model.RepeatePassword)
                    {
                        userRepository.Create(model.Name, model.Password);
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError(string.Empty, "Repeated password isn't equals password");
                }
            }
            catch (SQLiteException)
            {
                ModelState.AddModelError(string.Empty, "The user name already exist");
            }
            return View(model);
        }
    }
}