using WebClothes.Models;

namespace WebClothes.Irepository
{
    public interface ISaleOrderLine
    {
        List<SaleOrderLine> SaleOrders();
        SaleOrderLine GetSaleOrderLine(int id);
        List<SaleOrderLine> GetSaleOrders(int id);
		List<SaleOrderLine> OrderLineUser(int id,int UserId);

		void DeleteSaleOrderLine(SaleOrderLine line);
        void UpdateSaleOrderLine(SaleOrderLine line);
        void CreateSaleOrderLine(SaleOrderLine line);
    }
}
