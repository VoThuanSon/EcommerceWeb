namespace WebClothes.Irepository
{
    public interface IOrderUnitOfWork
    {
        ICustomerRepo CustomerRepo { get; }
        ISaleOrderRepo SaleOrderRepo { get; }
        IProvinceRepo ProvinceRepo { get; }
        IDistrictRepo districtRepo { get; }
        IWardRepo WardRepo { get; }
        
        ISaleOrderLine saleOrderLine { get; }
    }
}
