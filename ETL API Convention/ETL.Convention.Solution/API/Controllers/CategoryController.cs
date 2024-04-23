using Domain.Entities;
using Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Utilities.Constants;
namespace API.Controllers
{
    /// <summary>
    /// Category controller.
    /// </summary>
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork context;
        public CategoryController(IUnitOfWork context)
        {
            this.context = context;
        }
        /// <summary>
        /// Checks whether the product name is duplicate or not.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(RouteConstants.CreateCategory)]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            try
            {
                if (await IsCategoryDuplicate(category) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);
                category.DateCreated = DateTime.Now;
                category.IsDeleted = false;
                context.CategoryRepository.Add(category);
                await context.SaveChangesAsync();
                return CreatedAtAction("ReadCategoryByKey", new { key = category.CategoryId }, category);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: sc-api/categories
        /// </summary>
        /// <returns> Http status code: Ok.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadCategorys)]
        public async Task<IActionResult> ReadCategorys()
        {
            try
            {
                var categoryInDb = await context.CategoryRepository.GetCategorys();
                return Ok(categoryInDb);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// sc-api/category/key/{key}
        /// </summary>
        /// <param name="key"> Primary key of the table categories</param>
        /// <returns> Http status code: Ok.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadCategoryByKey)]
        public async Task<IActionResult> ReadCategoryByKey(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);
                var categoryInDb = await context.CategoryRepository.GetCategoryInfoByKey(key);
                if (categoryInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);
                return Ok(categoryInDb);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// sc-api/category/{key}
        /// </summary>
        /// <param name="key"></param>
        /// <param name="category"></param>
        /// <returns> Http status code: NoContent</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateCategory)]
        public async Task<IActionResult> UpdateCategory(int key, Category category)
        {
            try
            {
                if (key != category.CategoryId)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);
                if (await IsCategoryDuplicate(category) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);
                category.DateModified = DateTime.Now;
                category.IsDeleted = false;
                context.CategoryRepository.Update(category);
                await context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// sc-api/category/{key}
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Http status code: Ok.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteCategory)]
        public async Task<IActionResult> DeleteCategory(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);
                var categoryInDb = await context.CategoryRepository.GetCategoryInfoByKey(key);
                if (categoryInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);
                categoryInDb.DateModified = DateTime.Now;
                categoryInDb.IsDeleted = true;
                context.CategoryRepository.Update(categoryInDb);
                await context.SaveChangesAsync();
                return Ok(categoryInDb);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// checks whether the category name is duplicate or not.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        private async Task<bool> IsCategoryDuplicate(Category category)
        {
            try
            {
                var categoryInDb = await context.CategoryRepository.GetCategoryByName(category.CategoryName);
                if (categoryInDb != null)
                    if (categoryInDb.CategoryId != category.CategoryId)
                        return true;
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}