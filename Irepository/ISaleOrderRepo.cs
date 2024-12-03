using WebClothes.Models;

namespace WebClothes.Irepository
{
    public interface ISaleOrderRepo
    {
        List<SaleOrder> SaleOrders();
        int LastOrder();
        SaleOrder GetSaleOrder(int id);
        void Create(SaleOrder order);
        void Update(SaleOrder order);
        void Delete(SaleOrder order);

    }
}
