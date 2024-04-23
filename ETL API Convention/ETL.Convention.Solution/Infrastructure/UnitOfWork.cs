using Infrastructure.Contracts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        protected readonly ProductDbContext context;
        private bool _disposed;

        public UnitOfWork(ProductDbContext context)
        {
            this.context = context;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        #region Category
        private ICategoryRepository categoryRepository;
        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                categoryRepository = new CategoryRepository(context);
                return categoryRepository;
            }
        }
        #endregion

        #region Product
        private IProductRepository productRepository;
        public IProductRepository ProductRepository
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(context);
                return productRepository;
            }
        }
        #endregion
        
        public IDbContextTransaction BeginTransaction()
        {
            return context.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await context.Database.BeginTransactionAsync();
        }

        protected void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }        
    }
}