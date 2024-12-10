using Microsoft.EntityFrameworkCore;
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

        public SaleOrder SaleOrder(SaleOrder order)
        {
            return _shopContext.SaleOrders.FirstOrDefault(p => p.WardId ==  order.WardId && p.ProvinceId == order.ProvinceId && p.DistrictId == order.DistrictId && p.Date_Order == order.Date_Order && p.CustomerId == order.CustomerId && p.PaymentMethod == order.PaymentMethod);
        }

		public List<SaleOrder> SaleOrderUser(int id)
		{
            return _shopContext.SaleOrders.Where(p => p.CustomerId == id).ToList();
		}
	}
}
