using KM.BackOffice.Application.Repositories;
using KM.BackOffice.Core.Models;
using KM.BackOffice.Database.Entities;
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
            ViewBag.GetAllUsers = users;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> IndexPanel(int userId = 0)
        {
            var user = new UserModel();
            try
            {
                if (userId > 0)
                {
                    user = await _userRepository.getUsersByIdAsync(userId);
                    ViewBag.actions = "Update";
                }
                else
                {
                    ViewBag.actions = "Create";
                }
                ViewBag.useridx = userId;
            }
            catch
            {
                throw;
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserModel req)
        {
            try
            {
                bool user = false;

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (req != null)
                    user = await _userRepository.insertUsersAsync(req);


            }
            catch
            {
                throw;
            }
            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserModel req)
        {
            try
            {
                bool user;

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (req != null)
                    user = await _userRepository.updateUsersAsync(req);
            }
            catch
            {
                throw;
            }
            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int userId = 0)
        {
            try
            {

                if (userId > 0)
                {
                    var user = await _userRepository.deleteUserAsync(userId);
                }
            }
            catch
            {
                throw;
            }
            return RedirectToAction("Index", "User");
        }
    }
}
