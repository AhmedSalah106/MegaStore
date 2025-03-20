using MegaMarket.Repository;
using MegaMarket.Service;
using MegaMarket.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe.BillingPortal;
using WebApplication1.Service;
using Stripe.Checkout;
using Stripe;
using MegaMarket1.Models;
using Newtonsoft.Json;

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


        public IActionResult AddToCart(int id , int Quantity)
        {
           // MegaMarket1.Models.Product product = productService.GetById(ProductId);            
            MegaMarket1.Models.Product myProduct = productService.GetById(id);
            myProduct.Amount -= Quantity;
            productService.Update(myProduct);
            string CartJson = HttpContext.Session.GetString("ProductsCart");
            List<ProductCart> products = CartJson==null ? new List<ProductCart>() : JsonConvert.DeserializeObject<List<ProductCart>>(CartJson);

            if(products.Find(product => product.Id == id)!=null)
            {
                ProductCart ProductInCart = products.FirstOrDefault(product => product.Id == id);
                ProductInCart.Quantity += Quantity;

                products.RemoveAll(e => e.Id == id);
                products.Add(ProductInCart);
                HttpContext.Session.SetString("ProductsCart", JsonConvert.SerializeObject(products));
            }
            else
            {
                ProductCart product = productService.GetProductCart(id , Quantity);
                products.Add(product);
                HttpContext.Session.SetString("ProductsCart" , JsonConvert.SerializeObject(products));
                int Total = HttpContext.Session.GetInt32("ProductCount")??0;
                Total++;
                HttpContext.Session.SetInt32("ProductCount", Total);

            }



                return RedirectToAction("Index");
        }

        public IActionResult Cart()
        {
            string ProductJson = HttpContext.Session.GetString("ProductsCart");

            List<ProductCart> products = ProductJson == null ? new List<ProductCart>() : JsonConvert.DeserializeObject<List<ProductCart>>(ProductJson);

            return View("CartView" , products);
        }

        public IActionResult RemoveFromCart(int id)
        {

            string productJson = HttpContext.Session.GetString("ProductsCart");

            List<ProductCart>products = JsonConvert.DeserializeObject<List<ProductCart>>(productJson);


            products.RemoveAll(e =>e.Id == id);

            HttpContext.Session.SetString("ProductsCart", JsonConvert.SerializeObject(products));

            int Total = HttpContext.Session.GetInt32("ProductCount") ?? 0;
            Total--;

            HttpContext.Session.SetInt32("ProductCount", Total);

            return RedirectToAction("Cart"); 
        }
        public IActionResult CheckOut()
        {

            string ProductsJson = HttpContext.Session.GetString("ProductsCart");

            List<ProductViewModel> productsVM = ProductsJson == null ? new List<ProductViewModel>() : JsonConvert.DeserializeObject<List<ProductViewModel>>(ProductsJson);

            var domain = "http://localhost:5122/";
            var option = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = domain + $"Product/success",
                CancelUrl = domain + $"product/cancel",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment"
            };

            foreach (var item in productsVM)
            {
                var SessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price*100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name
                        }
                    },
                    Quantity = 1
                };
                option.LineItems.Add(SessionListItem);
            }

            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Create(option);

            TempData["Session"] = session.Id;

            Response.Headers.Add("Location",session.Url);

            return new StatusCodeResult(303);
        }

        public IActionResult OrderConfirmation()
        {
            var service =new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Get(TempData["Session"].ToString());
            if (session.PaymentStatus == "Paid")
                return RedirectToAction("Success");
            return RedirectToAction("Cancel");
        }

        public IActionResult Success()
        {
            return View("Success");
        }

        public IActionResult Cancel()
        {
            return View("Cancel");
        }




    }
}
