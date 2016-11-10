namespace BabyShop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private BabyShopDbContext dbContext;

        public BabyShopDbContext Init()
        {
            return dbContext ?? (dbContext = new BabyShopDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}