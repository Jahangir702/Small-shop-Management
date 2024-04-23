using Microsoft.EntityFrameworkCore.Storage;
namespace Infrastructure.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
    }
}