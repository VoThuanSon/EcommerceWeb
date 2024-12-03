using WebClothes.Models;

namespace WebClothes.Irepository
{
    public interface ISaleOrderLine
    {
        List<SaleOrderLine> SaleOrders();
        SaleOrderLine GetSaleOrderLine(int id);
        void DeleteSaleOrderLine(SaleOrderLine line);
        void UpdateSaleOrderLine(SaleOrderLine line);
        void CreateSaleOrderLine(SaleOrderLine line);
    }
}
