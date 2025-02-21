using MegaMarket.Models;
using MegaMarket.Repository;
using MegaMarket.Service;
using MegaMarket.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service;

namespace MegaMarket.Controllers
{
    public class AccountController : Controller
    {

        private readonly IVendorService vendorService;
        private readonly ISellerService sellerService;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        public AccountController(IVendorService _vendorService ,ISellerService _sellerService ,SignInManager<ApplicationUser>_signInManager ,UserManager<ApplicationUser>_userManager)
        {
            vendorService = _vendorService;
            signInManager = _signInManager;
            sellerService = _sellerService;
            userManager = _userManager;

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }


        public async Task<IActionResult> SaveRegister(RegisterViewModel RegisterVM)
        {
            if (ModelState.IsValid)
            {

                ApplicationUser user = new ApplicationUser();
                user.UserName = RegisterVM.Name;

                IdentityResult result = await userManager.CreateAsync(user , RegisterVM.Password);
                if (result.Succeeded)
                {

                    if (RegisterVM.IsVendor)
                    {
                        
                        Vendor vendor = new Vendor();  
                        vendor.Name = RegisterVM.Name;
                        vendor.User = user;
                        vendor.UserId = user.Id;
                        vendor.PhoneNumber = RegisterVM.Phone;

                        vendorService.Insert(vendor);
                        vendorService.Save();
                        await userManager.AddToRoleAsync(user, "Vendor");

                        await signInManager.SignInAsync(user, false);

                        return RedirectToAction("index","vendor");
                    }
                    else
                    {







                        Seller seller = new Seller();
          
                        seller.Name = RegisterVM.Name;
                        seller.User = user;
                        seller.UserId = user.Id;

                        sellerService.Insert(seller);
                        sellerService.Save();

                        await userManager.AddToRoleAsync(user, "Seller");

                        await signInManager.SignInAsync(user, false);

                        return RedirectToAction("Index", "product");
                  
                    }
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }





            return View("Register",RegisterVM);
        }
    }
}
