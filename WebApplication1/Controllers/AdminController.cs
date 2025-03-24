using MegaMarket.Service;
using MegaStore.Repository;
using MegaStore.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication1.Service;

namespace MegaStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly IOrderService orderService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProductService productService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IVendorService vendorService;
        public AdminController(IVendorService vendorService,
                               IOrderService orderService ,
                               UserManager<ApplicationUser> userManager,
                               IProductService productService,
                               RoleManager<IdentityRole>roleManager) 
        {
            this.vendorService = vendorService;
            this.roleManager = roleManager;
            this.productService = productService;
            this.userManager = userManager ;
            this.orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {

            AdminDashBoardViewModel adminDashBoard = new AdminDashBoardViewModel()
            {
                TotalSales = orderService.GetTotalPrice(),
                TotalUsers = userManager.Users.Count(),
                TotalProducts = productService.GetTotalProducts(),
                TotalOrders = orderService.GetTotalOrders(),
                ProductsInMarket = productService.GetAll(),
                vendors = vendorService.GetAll()
            };

            var user = await userManager.GetUserAsync(User); // Get the current logged-in user
            var roles = user != null ? await userManager.GetRolesAsync(user) : new List<string>();


            return View("Index" , adminDashBoard);
        }

    }
}
