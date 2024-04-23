using Domain.Entities;
namespace Infrastructure.Contracts
{
    public interface ICategoryRepository : IRepository<Category>
    {
        /// <summary>
        /// The method is used to get a CategoryInfo by key.
        /// </summary>
        /// <param name="key">Primary key of the table CategoryInfo.</param>
        /// <returns>Returns a CategoryInfo if the key is matched.</returns>
        public Task<Category> GetCategoryInfoByKey(int key);

        /// <summary>
        /// The method is used to get a Category by Category name.
        /// </summary>
        /// <param name="CategoryName">Name of a Category.</param>
        /// <returns>Returns a facility if the facility name is matched.</returns>
        public Task<Category> GetCategoryByName(string CategoryName);

        /// <summary>
        /// Returns all Category.
        /// </summary>
        /// <returns>List of Category object.</returns>
        public Task<IEnumerable<Category>> GetCategorys();
    }
}