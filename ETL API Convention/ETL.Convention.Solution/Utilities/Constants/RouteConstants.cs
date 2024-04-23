namespace Utilities.Constants
{
    public static class RouteConstants
    {
        public const string BaseRoute = "library-api";

        #region Category
        public const string CreateCategory = "category";

        public const string ReadCategorys = "categorys";

        public const string ReadCategoryByKey = "category/key/{key}";

        public const string UpdateCategory = "category/{key}";

        public const string DeleteCategory = "category/{key}";
        #endregion

        #region Product
        public const string CreateProduct = "product";

        public const string ReadProducts = "products";

        public const string ReadProductsByCategoryId = "products/category/{categoryId}";

        public const string ReadProductByKey = "product/key/{key}";

        public const string UpdateProduct = "product/{key}";

        public const string DeleteProduct = "Product/{key}";
        #endregion
    }
}