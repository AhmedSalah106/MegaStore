using MegaMarket.Repository;
using MegaMarket.Service;
using MegaMarket.ViewModel;
using MegaMarket1.Models;
using Microsoft.AspNetCore.Mvc;

namespace MegaMarket.Controllers
{
    public class VendorController : Controller
    {

        private readonly IVendorService vendorService;
        private readonly IProductService productService;
        public VendorController(IVendorService _vendorService)
        {
            vendorService = _vendorService;
        }
        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult AddProduct()
        {
            return View("AddProduct");
        }

        public IActionResult SaveProduct(ProductViewModel productVM) 
        {
            if(ModelState.IsValid) 
            {
                Product product = productService.GetProduct(productVM);
                productService.Insert(product);
                productService.Save();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Invalid Data");
            return View("AddProduct",productVM);
        }

    }
}
