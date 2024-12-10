using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebClothes.Data;
using WebClothes.ViewModels;

namespace WebClothes.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ShopContext _shopContext;

        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ShopContext context)
        {
            _userManager = userManager; _roleManager = roleManager;
            _shopContext = context;
        }
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var userRolesViewModel = new List<UserRolesViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRolesViewModel.Add(new UserRolesViewModel { UserId = user.Id, UserName = user.UserName, Roles = roles });
            }
            return View(userRolesViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {

            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();
                var mode = new UserRolesViewModel { UserId = id, UserName = user.UserName, Roles = roles, AvailableRoles = allRoles };
                return View(mode);
            }
            return NotFound("User not found");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            user.UserName = model.UserName;
            var userRoles = await _userManager.GetRolesAsync(user);
            var rolesToAdd = model.Roles.Except(userRoles);
            var rolesToRemove = userRoles.Except(model.Roles);
            await _userManager.AddToRolesAsync(user, rolesToAdd);
            await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index", "Home", "Admin");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await _shopContext.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Account", "Admin"); ;
        }
    }
}
