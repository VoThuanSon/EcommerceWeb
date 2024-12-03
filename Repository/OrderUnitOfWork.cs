using WebClothes.Data;
using WebClothes.Irepository;

namespace WebClothes.Repository
{
    public class OrderUnitOfWork : IOrderUnitOfWork
    {
        private readonly ShopContext _shopContext;
        private ICustomerRepo _customerRepo;
        private ISaleOrderRepo _saleOrderRepo;
        private IProvinceRepo _provinceRepo;
        private IDistrictRepo _districtRepo;
        private IWardRepo _wardRepo;
        private ISaleOrderLine _saleOrderLine;
      
        public OrderUnitOfWork(ShopContext context)
        {
            _shopContext = context;
        }
        public ICustomerRepo CustomerRepo
        {
            get { 
                
                return _customerRepo = _customerRepo ?? new CustomerRepo(_shopContext); 
            }  
        }
        public ISaleOrderRepo SaleOrderRepo
        {
            get
            {
                return _saleOrderRepo = _saleOrderRepo ?? new SaleOrderRepo(_shopContext);
            }
        }
        public IProvinceRepo ProvinceRepo
        {
            get
            {
                return _provinceRepo = _provinceRepo ?? new ProvinceRepo(_shopContext);
            }
        }
        public IDistrictRepo districtRepo
        {
            get
            {
                return _districtRepo = _districtRepo ?? new DistrictRepo(_shopContext);
            }
        }
        public IWardRepo WardRepo
        {
            get
            {
                return _wardRepo = _wardRepo ?? new WardRepo(_shopContext); 
            }
        }

        public ISaleOrderLine saleOrderLine
        {
            get
            {
                return _saleOrderLine = _saleOrderLine ?? new SaleOrderLineRepo(_shopContext);
            }
        }
    }
}
