using WebClothes.Data;
using WebClothes.Irepository;
using WebClothes.Models;

namespace WebClothes.Repository
{
    public class SaleOrderRepo : ISaleOrderRepo
    {
        private readonly ShopContext _shopContext;

        public SaleOrderRepo(ShopContext context)
        {
            _shopContext = context;
        }
        public void Create(SaleOrder order)
        {
            _shopContext.SaleOrders.Add(order);
            _shopContext.SaveChanges();
            
        }
        public int LastOrder()
        {
            var pro = _shopContext.SaleOrderLines.OrderBy(p => p.Id).LastOrDefault();
            if (pro != null)
            {
                return pro.Id + 1;
            }
            return 1;
        }


        public void Delete(SaleOrder order)
        {
            _shopContext.SaleOrders.Remove(order);
            _shopContext.SaveChanges();
        }

        public List<SaleOrder> SaleOrders()
        {
            return _shopContext.SaleOrders.ToList();
        }

        public void Update(SaleOrder order)
        {
            _shopContext.SaleOrders.Update(order);
            _shopContext.SaveChanges();
        }

        public SaleOrder GetSaleOrder(int id)
        {
            return _shopContext.SaleOrders.FirstOrDefault(p => p.Id == id);
        }
    }
}
