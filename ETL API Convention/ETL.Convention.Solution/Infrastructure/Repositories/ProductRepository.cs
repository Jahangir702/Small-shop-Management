using Domain.Entities;
using Infrastructure.Contracts;

namespace Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ProductDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Product>> GetProductInfoByDepartment(int CategoryId)
        {
            try
            {
                var productInDb = await QueryAsync(p => p.IsDeleted == false && p.CategoryId == CategoryId, d => d.Categorys);

                return productInDb.OrderBy(d => d.ProductName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> GetProductInfoByKey(int key)
        {
            try
            {
                return await FirstOrDefaultAsync(c => c.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> GetProductInfoByName(string ProductInfoName)
        {
            try
            {
                return await FirstOrDefaultAsync(c => c.ProductName.ToLower().Trim() == ProductInfoName.ToLower().Trim() && c.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetProductInfos()
        {
            try
            {
                return await LoadListWithChildAsync<Product>(c => c.IsDeleted == false, d => d.Categorys);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}