using System;

namespace BabyShop.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        BabyShopDbContext Init();
    }
}