using KM.BackOffice.Application.Repositories;
using KM.BackOffice.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KM.BackOffice.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.getAllUsersAsync();
            return View(users);
        }

        public IActionResult _UpdatePanel()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserReq req)
        {
            try
            {
                if (req != null)
                {
                    var user = await _userRepository.insertUsersAsync(req);
                }
            }
            catch
            {
                throw;
            }
            return RedirectToPage("/Index");
        }
    }
}
