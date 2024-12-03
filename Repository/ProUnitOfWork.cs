using WebClothes.Data;
using WebClothes.Irepository;

namespace WebClothes.Repository
{
    public class ProUnitOfWork : IProUnitOfWork
    {
        private readonly ShopContext _shopContext;
        private IProUomRepo _proUomRepo;
        private IImgRepo _imgRepo;
        private IAttrRepo _attrRepo;
        private IAttr_VaRepo _attr_vaRepo;
        private IProductRepo _productRepo;
        private ICategoryRepo _categoryRepo;
        private IAttrAndPro _AttrAndPro;

        public ProUnitOfWork(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        public IProductRepo Product
        {
            get
            {
                return _productRepo = _productRepo ?? new ProductRepo(_shopContext);
            }
        }

        public ICategoryRepo Category
        {
            get
            {
                return _categoryRepo = _categoryRepo ?? new CategoryRepo(_shopContext);
            }
            

        } 

        public IAttrRepo Attr
        {
            get
            {
                return _attrRepo = _attrRepo ?? new AttrRepo(_shopContext);
            }
        }

        public IAttr_VaRepo Attr_Va
        {
            get
            {
                return _attr_vaRepo = _attr_vaRepo ?? new Attr_VaRepo(_shopContext);
            }
        }

        public IImgRepo imgRepo
        {
            get
            {
                return _imgRepo = _imgRepo ?? new ImgRepo(_shopContext);
            }
        } 

        public IProUomRepo proUomRepo
        {
            get
            {
                return _proUomRepo = _proUomRepo ?? new ProUomRepo(_shopContext);
            }
        }
        public IAttrAndPro AttrAndPro
        {
            get
            {
                return _AttrAndPro = _AttrAndPro ?? new AttrAndProRepo(_shopContext);
            }
        }
    }
}
