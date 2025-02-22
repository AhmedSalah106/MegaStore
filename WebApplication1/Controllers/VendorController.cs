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
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IVendorService vendorService;
        private readonly IProductService productService;
        private readonly UserManager<ApplicationUser>  userManager;
        public VendorController(IWebHostEnvironment _webHostEnvironment,
            IVendorService _vendorService ,
            IProductService _productService ,
            UserManager<ApplicationUser>_userManager)
        {
            webHostEnvironment = _webHostEnvironment;
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
        [HttpPost]
        public async Task<IActionResult> SaveProduct(IFormFile ImageFile ,ProductViewModel productVM) 
        {
            if(ModelState.IsValid) 
            {
                Product product = productService.GetProduct(productVM);

                ApplicationUser user = await userManager.GetUserAsync(User);
                string userName = user.UserName;

                productVM.VendorId = vendorService.GetIdByName(userName);

                string wRootPath = Path.Combine(webHostEnvironment.WebRootPath, "Images");

                string imageName =  Guid.NewGuid().ToString()+"_"+ ImageFile.FileName;
                string imagePath = Path.Combine(wRootPath, imageName);

                using(FileStream fileStream = new FileStream(imagePath,FileMode.Create))
                {
                    ImageFile.CopyTo(fileStream);
                }

                product.ImageURL = Path.Combine("/Images" , imageName);

                productService.Insert(product);
                productService.Save();

                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Invalid Data");
            return View("AddProduct",productVM);
        }

    }
}
