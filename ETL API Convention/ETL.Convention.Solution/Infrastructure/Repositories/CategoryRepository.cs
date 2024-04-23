using Domain.Entities;
using Infrastructure.Contracts;
namespace Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ProductDbContext context) : base(context)
        {

        }

        public async Task<Category> GetCategoryInfoByKey(int key)
        {
            try
            {
                return await FirstOrDefaultAsync(c => c.CategoryId == key && c.Available == true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Category> GetCategoryByName(string CategoryName)
        {
            try
            {
                return await FirstOrDefaultAsync(c => c.CategoryName.ToLower().Trim() == CategoryName.ToLower().Trim() && c.Available== false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Task<int> GetCategoryCount()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetCategorys()
        {
            try
            {
                return await QueryAsync(p=> p.Available == true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}