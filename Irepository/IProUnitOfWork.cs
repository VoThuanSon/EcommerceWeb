namespace WebClothes.Irepository
{
    public interface IProUnitOfWork
    {
        IProductRepo Product { get; }
        ICategoryRepo Category { get; }
        IAttrRepo Attr { get; }
        IAttr_VaRepo Attr_Va { get; }
        IImgRepo imgRepo { get; }
        IProUomRepo proUomRepo { get; }
        IAttrAndPro AttrAndPro { get; }
    }
}
