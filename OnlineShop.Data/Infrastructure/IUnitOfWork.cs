namespace BabyShop.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}