using MegaMarket.Models;
using MegaMarket.Repository;
using MegaMarket.Service;
using MegaMarket.ViewModel;
using MegaMarket1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service;

namespace MegaMarket.Controllers
{
    public class VendorController : Controller
    {

        private readonly IVendorService vendorService;
        private readonly IProductService productService;
        private readonly UserManager<ApplicationUser>  userManager;
        public VendorController(IVendorService _vendorService ,
            IProductService _productService ,
            UserManager<ApplicationUser>_userManager)
        {
            productService = _productService;
            vendorService = _vendorService;
            userManager = _userManager;
        }
        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult AddProduct()
        {
            return View("AddProduct");
        }

        public async Task<IActionResult> SaveProduct(ProductViewModel productVM) 
        {
            if(ModelState.IsValid) 
            {
                Product product = productService.GetProduct(productVM);

                ApplicationUser user = await userManager.GetUserAsync(User);
                string userName = user.UserName;

                productVM.VendorId = vendorService.GetIdByName(userName);

                productService.Insert(product);
                productService.Save();

                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Invalid Data");
            return View("AddProduct",productVM);
        }

    }
}
