using MegaMarket.Models;
using MegaMarket.Repository;
using MegaMarket.Service;
using MegaMarket.ViewModel;
using MegaMarket1.Models;
using MegaStore.Hubs;
using MegaStore.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using WebApplication1.Service;

namespace MegaMarket.Controllers
{
    public class VendorController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IVendorService vendorService;
        private readonly IProductService productService;
        private readonly UserManager<ApplicationUser>  userManager;
        private readonly IHubContext<ProductHub> hubClients;

        public VendorController(IWebHostEnvironment _webHostEnvironment,
            IVendorService _vendorService ,
            IProductService _productService ,
            UserManager<ApplicationUser>_userManager,
            IHubContext<ProductHub>_hubClients)
        {
            webHostEnvironment = _webHostEnvironment;
            productService = _productService;
            vendorService = _vendorService;
            userManager = _userManager;
            hubClients = _hubClients;
        }

        [Authorize(Roles ="Vendor")]
        public IActionResult Index()
        {

            int id = GetVendorId();
            VendorProductsViewModel products = vendorService.GetVendorProductsVM(id);
       
            return View("index" , products);  
        }

        private int GetVendorId( )
        {
            return vendorService.GetIdByName(GetVendorName());
        }

        private string GetVendorName()
        {
            return User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Name).Value.ToString();
        }

        [Authorize(Roles ="Vendor")]
        public async Task<IActionResult> AddProduct()
        {
            return View("AddProduct");
        }

        [HttpPost]
        [Authorize(Roles ="Vendor")]
        public async Task<IActionResult> SaveProduct(IFormFile ImageFile ,ProductViewModel productVM) 
        {
            if(ModelState.IsValid) 
            {
                Product product = productService.GetProduct(productVM);

                string userName = GetVendorName();

                product.VendorId = vendorService.GetIdByName(userName);

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



                hubClients.Clients.All.SendAsync("AddProductNotify", product);



                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Invalid Data");
            return View("AddProduct",productVM);
        }
        [Authorize(Roles ="Vendor")]
        public IActionResult Edit(int id)
        {
            ProductViewModel productVM = productService.GetProductViewModel(id);
            return View("Edit" , productVM);
        }

        [Authorize(Roles ="Vendor")]
        public IActionResult SaveEdit(int id,ProductViewModel productVM  )
        {
            if (ModelState.IsValid)
            {
                Product product = productService.GetUpdated(id,productVM );
                productService.Update(product);
                productService.Save();
                return RedirectToAction("index");
            }



            ModelState.AddModelError(string.Empty, "invalid Data enter");
            return View("Edit", productVM);
        }


        [Authorize(Roles ="Vendor")]
        public IActionResult Delete(int Id)
        {
            if(ModelState.IsValid)
            {

                string imagePath = productService.GetById(Id).ImageURL.TrimStart('/');

                string wrootPath = Path.Combine(webHostEnvironment.WebRootPath, imagePath);

                if (System.IO.File.Exists(wrootPath))
                {
                    System.IO.File.Delete(wrootPath);
                }
                
                productService.Delete(Id);
                productService.Save();
                return RedirectToAction("Index");
            }
            return View("index","product");
        }

    }
}
