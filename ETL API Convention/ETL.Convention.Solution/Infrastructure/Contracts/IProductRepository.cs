using Domain.Entities;
namespace Infrastructure.Contracts
{
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// The method is used to get a ProductInfo by ProductInfo name.
        /// </summary>
        /// <param name="ProductInfoName">Name of a ProductInfo.</param>
        /// <returns>Returns a facility if the facility name is matched.</returns>
        public Task<Product> GetProductInfoByName(string ProductInfoName);

        /// <summary>
        /// The method is used to get a ProductInfo by key.
        /// </summary>
        /// <param name="key">Primary key of the table ProductInfos.</param>
        /// <returns>Returns a ProductInfo if the key is matched.</returns>
        public Task<Product> GetProductInfoByKey(int key);

        /// <summary>
        /// The method is used to get the ProductInfo byShelfId.
        /// </summary>
        /// <param name="ShelfId">PovinceID of the table ProductInfos.</param>
        /// <returns>Returns a ProductInfo if theShelfId is matched.</returns>
        public Task<IEnumerable<Product>> GetProductInfoByDepartment(int CategoryId);

        /// <summary>
        /// The method is used to get the list of ProductInfos.
        /// </summary>
        /// <returns>Returns a list of all ProductInfos.</returns>
        public Task<IEnumerable<Product>> GetProductInfos();
    }
}