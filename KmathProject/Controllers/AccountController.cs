using KM.BackOffice.Application.Repositories;
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
        public async Task<IActionResult> SignIn(string username, string password)
        {
            var signIn = await _accountRepository.SignIn(username, password);
            if (signIn)
                return RedirectToAction("Index", "User");

            return RedirectToAction("Login", "Account");
        }

    }
}
