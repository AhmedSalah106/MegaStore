using MegaMarket.Models;
using MegaMarket.Repository;

namespace MegaMarket.Service
{
    public class SellerService:ISellerService
    {
        private readonly ISellerRepository sellerRepository;
        public SellerService(ISellerRepository _sellerRepository)
        {
            sellerRepository = _sellerRepository;
        }

        public List<Seller> GetAll(string Includes = null)
        {
            return sellerRepository.GetAll();
        }

        public Seller GetById(int Id, string Includes = null)
        {
            return sellerRepository.GetById(Id);
        }
        public void Insert(Seller seller)
        {
            seller.Name.ToLower();
            sellerRepository.Insert(seller);
        }
        public void Update(int Id)
        {
            sellerRepository.Update(Id);
        }
        public void Delete(int Id)
        {
            sellerRepository.Delete(Id);
        }
        public void Save()
        {
            sellerRepository.Save();
        }
    }
}
