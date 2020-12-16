using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.Models.Identity;
using CourseWork.ViewModels.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    [Authorize(Roles = Role.Admin)]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IList<UserViewModel> users = new List<UserViewModel>();
            foreach (var user in _userManager.Users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault(r => Role.Admin == r) ?? Role.User;
                users.Add(new UserViewModel {User = user, Role = role});
            }

            return View(users);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null || user.UserName.Equals(User.Identity.Name)) return RedirectToAction("Index");
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault(r => Role.Admin == r) ?? Role.User;
            switch (role)
            {
                case Role.Admin:
                    await _userManager.AddToRoleAsync(user, Role.User);
                    await _userManager.RemoveFromRoleAsync(user, Role.Admin);
                    break;
                case Role.User:
                    await _userManager.AddToRoleAsync(user, Role.Admin);
                    await _userManager.RemoveFromRoleAsync(user, Role.User);
                    break;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null && !user.UserName.Equals(User.Identity.Name)) await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }
    }
}