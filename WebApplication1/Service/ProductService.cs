using MegaMarket.Repository;
using MegaMarket.ViewModel;
using MegaMarket1.Models;

namespace MegaMarket.Service
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository productRepository;
        public ProductService(IProductRepository _productRepository)
        {
            productRepository = _productRepository;   
        }
        public void Delete(int Id)
        {
            productRepository.Delete(Id);
        }

        public List<Product> GetAll(string Includes = null)
        {
            return productRepository.GetAll(Includes);
        }

        public Product GetById(int Id, string Includes = null)
        {
            return productRepository.GetById(Id, Includes);
        }

        public Product GetProduct(ProductViewModel model)
        {
            Product product = new Product();
            product.Brand = model.Brand;
            product.Name = model.Name;  
            product.Description = model.Description;
            product.Amount = model.Amount;
            product.VendorId = 1;
            product.CategoryId = 1;
            return product;
        }

        public List<ProductViewModel>GetAllProductViewModel()
        {
            List<Product> products = GetAll();

            List<ProductViewModel>productsVM = products.Select
                (e=> new ProductViewModel{ Name = e.Name, Description = e.Description,
                    Price = e.Price ,VendorId = e.VendorId , Brand = e.Brand ,
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

        public void Update(int Id)
        {
            productRepository.Update(Id);
        }
    }
}
