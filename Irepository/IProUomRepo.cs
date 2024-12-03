using WebClothes.Models;

namespace WebClothes.Irepository
{
    public interface IProUomRepo
    {
        List<ProductUom> GetProOum();
        void Create(ProductUom product);
        void Update(ProductUom product);
        void Delete(int? id);

        ProductUom FindIdOum(int? id);
    }
}
