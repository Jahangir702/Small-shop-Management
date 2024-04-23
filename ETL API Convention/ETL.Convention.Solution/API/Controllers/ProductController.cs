using Domain.Entities;
using Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Utilities.Constants;
namespace API.Controllers
{
    /// <summary>
    /// Product Controller
    /// </summary>
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork context;
        public ProductController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// sc-api/product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Http status code: CreateAT.</returns>
        [HttpPost]
        [Route(RouteConstants.CreateProduct)]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            try
            {
                product.DateCreated = DateTime.Now;
                product.IsDeleted = false;
                context.ProductRepository.Add(product);
                await context.SaveChangesAsync();
                return CreatedAtAction("ReadProductByKey", new { key = product.ProductId }, product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// sc-api/products
        /// </summary>
        /// <returns>Http status code: Ok.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadProducts)]
        public async Task <IActionResult> ReadProducts()
        {
            try
            {
                var productIndb = await context.ProductRepository.GetProductInfos();
                return Ok(productIndb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// sc-api/product/key/{key}
        /// </summary>
        /// <param name="key"> Primary key of the table Products</param>
        /// <returns>Http status code: Ok.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadProductByKey)]
        public async Task<IActionResult> ReadProductByKey(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);
                var productIndb = await context.ProductRepository.GetProductInfoByKey(key);
                if (productIndb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);
                return Ok(productIndb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// sc-api/product/{key}
        /// </summary>
        /// <param name="key"></param>
        /// <param name="product"></param>
        /// <returns>Http Status code: NoContent</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateProduct)]
        public async Task<IActionResult> UpdateProduct(int key, Product product)
        {
            try
            {
                var productIndb = await context.ProductRepository.GetProductInfoByKey(product.ProductId);
                if (key != product.ProductId)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);
                productIndb.ProductName = product.ProductName;
                productIndb.Price = product.Price;
                productIndb.CategoryId = product.CategoryId;
                product.IsDeleted = false;
                context.ProductRepository.Update(product);
                await context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: sc-api/country/{key}
        /// </summary>
        /// <param name="key">Primary key of the table products</param>
        /// <returns>Http status code: Ok.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteProduct)]
        public async Task<IActionResult> DeleteProduct(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);
                var productIndb = await context.ProductRepository.GetProductInfoByKey(key);
                if (productIndb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);
                productIndb.DateModified = DateTime.Now;
                productIndb.IsDeleted = true;
                context.ProductRepository.Update(productIndb);
                await context.SaveChangesAsync();
                return Ok(productIndb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }
    }
}