using Microsoft.EntityFrameworkCore;
using WebClothes.Data;
using WebClothes.Irepository;
using WebClothes.Models;

namespace WebClothes.Repository
{
    public class SaleOrderLineRepo : ISaleOrderLine
    {
        private readonly ShopContext _shopContext;
        public SaleOrderLineRepo(ShopContext context)
        {
            _shopContext = context;
        }
        public void CreateSaleOrderLine(SaleOrderLine line)
        {
            _shopContext.SaleOrderLines.Add(line);
            _shopContext.SaveChanges();
        }

        public void DeleteSaleOrderLine(SaleOrderLine line)
        {
            _shopContext.SaleOrderLines.Remove(line);
            _shopContext.SaveChanges();
        }

        public SaleOrderLine GetSaleOrderLine(int id)
        {
            return _shopContext.SaleOrderLines.FirstOrDefault(p => p.Id == id);
        }

        public List<SaleOrderLine> GetSaleOrders(int id)
        {
            return _shopContext.SaleOrderLines.Where(p => p.SaleOrderId == id).ToList();
        }

		public List<SaleOrderLine> OrderLineUser(int id, int UserId)
		{
            return _shopContext.SaleOrderLines.Where(p => p.SaleOrderId == id && p.CustomerId == UserId).Include(p => p.Product).ToList();  
		}

		public List<SaleOrderLine> SaleOrders()
        {
            return _shopContext.SaleOrderLines.Include(p => p.Product).ToList();
        }

        public void UpdateSaleOrderLine(SaleOrderLine line)
        {
            _shopContext.SaleOrderLines.Update(line);   
            _shopContext.SaveChanges();
        }
    }
}
