using MegaMarket.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MegaMarket.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole>_roleManager)
        {
            roleManager = _roleManager;
        }
        [HttpGet]
        [Authorize(Roles="Admin")]
        public IActionResult AddRole()
        {
            return View("Create");
        }
        [HttpPost]
        public async Task<IActionResult> SaveCreate(RoleViewModel RoleVM)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = RoleVM.RoleName;

                IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index","Admin");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid Name");
            return View("Create", RoleVM);
        }
    }
}
