using MegaMarket.Repository;
using MegaMarket.ViewModel;
using MegaMarket1.Models;

namespace MegaMarket.Service
{
    public class ProductService : IProductService
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IProductRepository productRepository;
        public ProductService(IProductRepository _productRepository , IWebHostEnvironment _webHostEnvironment)
        {
            productRepository = _productRepository; 
            webHostEnvironment = _webHostEnvironment;
        }
        public void Delete(int Id)
        {
            productRepository.Delete(Id);
        }

        public List<Product> GetAll(string Includes = null)
        {
            return productRepository.GetAll(Includes);
        }

        public ProductViewModel GetProductViewModel(int Id)
        {
            Product product = GetById(Id);
            ProductViewModel productVM = new ProductViewModel();
            productVM.Id = Id;
            productVM.Name = product.Name;
            productVM.Description = product.Description;
            productVM.Brand = product.Brand;
            productVM.Price = product.Price;
            productVM.imageURL = product.ImageURL;
            productVM.CategoryId = product.CategoryId;
            productVM.Amount = product.Amount;
            productVM.VendorId = product.VendorId;
            return productVM;
        }
        public Product GetById(int Id, string Includes = null)
        {
            return productRepository.GetById(Id, Includes);
        }


        public Product GetUpdated(int id, ProductViewModel productVM )
        {
            Product product = GetById(id);
            product.Description = productVM.Description;
            product.Brand = productVM.Brand;
            product.Price = productVM.Price;
            product.CategoryId = productVM.CategoryId;
            product.Amount = productVM.Amount;
            return product;
        }
        public Product GetProduct(ProductViewModel model)
        {
            Product product = new Product();
            product.Brand = model.Brand;
            product.Name = model.Name;  
            product.Description = model.Description;
            product.Amount = model.Amount;
            product.Price = model.Price;
            product.VendorId = model.VendorId;
            product.CategoryId = 1;
            return product;
        }

        public List<ProductViewModel>GetAllProductViewModel()
        {
            List<Product> products = GetAll();

            List<ProductViewModel>productsVM = products.Select
                (e=> new ProductViewModel{ Id = e.Id , Name = e.Name, Description = e.Description,
                    Price = e.Price ,VendorId = e.VendorId , Brand = e.Brand , imageURL = e.ImageURL ,
                    Amount = e.Amount , CategoryId = e.CategoryId}).ToList();

            return productsVM;
        }

        public void Insert(Product product)
        {
            productRepository.Insert(product);
        }

        public void Save()
        {
            productRepository.Save();
        }

        public void Update(Product product)
        {
            productRepository.Update(product);
        }

        public ProductCart GetProductCart(int Id , int Quantity)
        {
             Product product = GetById(Id);
            ProductCart productCart = new ProductCart()
            {
                Id = Id ,
                Name = product.Name,
                ImageUrl = product.ImageURL,
                Quantity = Quantity,
                Price = product.Price * Quantity
            };
            return productCart;
        }

        public int GetTotalProducts()
        {
            return productRepository.GetTotalProducts();
        }
    }
}
