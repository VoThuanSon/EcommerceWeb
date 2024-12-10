using WebClothes.Models;

namespace WebClothes.Irepository
{
    public interface ISaleOrderRepo
    {
        List<SaleOrder> SaleOrders();
        SaleOrder SaleOrder(SaleOrder order);
		List<SaleOrder> SaleOrderUser(int id);

		SaleOrder GetSaleOrder(int id);
        void Create(SaleOrder order);
        void Update(SaleOrder order);
        void Delete(SaleOrder order);

    }
}
