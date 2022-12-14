using System.Collections.Generic;
using System.Linq;
using Shops.Models;
namespace Shops.Repositories
{
    public class ShopRepository
    {
        private readonly List<Shop> _listShops;

        public ShopRepository()
        {
            _listShops = new List<Shop>();
        }

        public List<Shop> GetListShop()
        {
            return _listShops;
        }

        public List<ShopProduct> GetListProductFromShop(Shop shop)
        {
            Shop shop1 = _listShops.FirstOrDefault(shop1 => shop1.ShopId.Equals(shop.ShopId));
            return shop1.Product;
        }

        public void AddShops(Shop shop)
        {
            _listShops.Add(shop);
        }
    }
}