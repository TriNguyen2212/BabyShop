using BabyShop.Data.Infrastructure;
using BabyShop.Model.Models;

namespace BabyShop.Data.Reponsitories
{
    public interface IProductResository
    {
    }

    public class ProductResository : RepositoryBase<Product>
    {
        public ProductResository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}