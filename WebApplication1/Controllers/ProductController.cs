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

        public IActionResult CheckOut()
        {
            List<ProductViewModel> productsVM = productService.GetAllProductViewModel().Take(1).ToList();

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
                        UnitAmount = (long)(item.Price),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name
                        }
                    },
                    Quantity = item.Amount
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
