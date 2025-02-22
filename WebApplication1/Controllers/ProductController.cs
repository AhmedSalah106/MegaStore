using MegaMarket.Repository;
using MegaMarket.Service;
using MegaMarket.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service;

namespace MegaMarket.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService _productService )
        {
            productService = _productService;
        }

        [Authorize(Roles ="Seller")]
        public IActionResult Index()
        {
            List<ProductViewModel> productsVM = productService.GetAllProductViewModel();
            return View("index" , productsVM);
        }

    }
}
