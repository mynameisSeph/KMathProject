using KM.BackOffice.Application.Repositories;
using KmathProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace KM.BackOffice.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountRepository _accountRepository;

        public AccountController(AccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel user)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Account/Login.cshtml", user);


            var signIn = await _accountRepository.SignInAsync(user.Username, user.Password);
            if (signIn)
                return RedirectToAction("Index", "User");

            return RedirectToAction("Login", "Account");
        }

    }
}
