using MegaMarket.Models;
using MegaMarket.Repository;
using MegaMarket.ViewModel;
using MegaMarket1.Models;
using MegaStore.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using WebApplication1.Service;

namespace MegaMarket.Service
{
    public class VendorService:IVendorService
    {
        private readonly IVendorRepository vendorRepository;
        private readonly IProductService productService;
        public VendorService(IVendorRepository _vendorRepository ,IProductService _productService )
        {
            vendorRepository = _vendorRepository;
            productService = _productService;
        }

        public List<Vendor> GetAll(string Includes = null)
        {
            return vendorRepository.GetAll();
        }
        public decimal GetTotalSailed(int Id)
        {
            return vendorRepository.GetById(Id).TotalSaled;
        }
        public List<Product>GetProductsByVendorId(int Id)
        {
             return productService.GetAll().Where(e=>e.VendorId == Id).ToList();
        }
        public List<ProductViewModel>GetAllProductsVM(List<Product>products)
        {
            List<ProductViewModel> productsVM = products.Select(e => new ProductViewModel {
                Id = e.Id,
                Name = e.Name ,
                Description = e.Description,
                Price = e.Price,
                Brand = e.Brand,
                imageURL = e.ImageURL,
                CategoryId = 1
                ,Amount = e.Amount,
                 VendorId = e.VendorId}).ToList();

            return productsVM;
        }
        public Vendor GetById(int Id,string Includes = null)
        {
            return vendorRepository.GetById(Id);
        }
        public void Insert(Vendor vendor)
        {
            vendor.Name.ToLower();
            vendorRepository.Insert(vendor);
        }
        public void Update(int Id)
        {
            vendorRepository.Update(Id);
        }
        public void Delete(int Id)
        {
            vendorRepository.Delete(Id);
        }
        public void Save()
        {
            vendorRepository.Save();
        }

        public VendorProductsViewModel GetVendorProductsVM(int Id)
        {
            VendorProductsViewModel vendorProductsVM = 
                new VendorProductsViewModel();
            Vendor vendor = vendorRepository.GetById(Id);
            vendorProductsVM.TotalSailed = vendor.TotalSaled;
            vendorProductsVM.vendorProducts = 
                GetAllProductsVM(GetProductsByVendorId(Id));
            return  vendorProductsVM;
        }
        public int GetIdByName(string Name)
        {
                return vendorRepository.GetVendorByName(Name).Id;
        }
    }

}
