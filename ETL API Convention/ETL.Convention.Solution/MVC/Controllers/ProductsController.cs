using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.HttpServices;
using Utilities.Constants;

namespace MVC.Controllers

{
    public class ProductsController : Controller
    {
        private readonly ProductsHttpService productsHttpService;
        private readonly IHttpContextAccessor httpContextAccessor;

        private ISession? session => httpContextAccessor.HttpContext?.Session;

        public ProductsController(ProductsHttpService productsHttpService, IHttpContextAccessor httpContextAccessor)
        {
            this.productsHttpService = productsHttpService;
            this.httpContextAccessor = httpContextAccessor;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var outcome = await productsHttpService.ReadProducts();

                if (outcome.ResponseStatus == ResponseStatus.Success)
                {
                    return View(outcome.EntityList);
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            //if (categoryId == 0)
            //    return RedirectToAction("Index", "Products");

            //ViewBag.CategoryId = categoryId;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            ResponseOutcome<Product> productOutcome = await productsHttpService.CreateProduct(product);

            if (productOutcome.ResponseStatus == ResponseStatus.Failed)
            {
                TempData[SessionConstants.Message] = productOutcome.Message;
            }

            if (productOutcome.ResponseStatus == ResponseStatus.Success)
            {
                TempData[SessionConstants.Message] = MessageConstants.ProductCreatedSuccessfully;

                return RedirectToAction("Index", new { id = productOutcome.Entity.CategoryId });
            }

            return View();
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int id)
        {
            ResponseOutcome<Product> productOutcome = await productsHttpService.ReadProductById(id);
            if (productOutcome.ResponseStatus == ResponseStatus.Success)
            {
                int categoryId = productOutcome.Entity.CategoryId;
                ViewBag.CategoryId = categoryId;

                return View(productOutcome.Entity);
            }
            else
            {
                return NotFound();
            }
        }
        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int id)
        {
            ResponseOutcome<Product> productOutcome = await productsHttpService.ReadProductById(id);

            //ViewBag.Categorys = new SelectList(await ReadCategorys(), FieldConstants.Oid, FieldConstants.CategoryName);

            if (productOutcome.ResponseStatus == ResponseStatus.Success)
                return View(productOutcome.Entity);
            else
                return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            ResponseOutcome<Product> productOutcome = await productsHttpService.UpdateProduct(product);

            if (productOutcome.ResponseStatus == ResponseStatus.Failed)
            {
                TempData[SessionConstants.Message] = productOutcome.Message;
                //ViewBag.Categorys = new SelectList(await ReadCategorys(), FieldConstants.Oid, FieldConstants.CategoryName);
            }

            if (productOutcome.ResponseStatus == ResponseStatus.Success)
            {
                TempData[SessionConstants.Message] = MessageConstants.ProductUpdatedSuccessfully;

                return RedirectToAction("Details", new { id = productOutcome.Entity.ProductId });
            }

            return View(product);
        }
        #endregion

        #region Read
        //private async Task<List<Category>> ReadCategorys()
        //{
        //    try
        //    {

        //        List<Category> categories = new List<Category>();

        //        ResponseOutcome<Category> categoryOutcome = await productsHttpService.id();

        //        if (categoryOutcome.ResponseStatus == ResponseStatus.Success)
        //            categories = categoryOutcome.EntityList;

        //        return categories;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        //private async Task<List<Category>> ReadCategorys()
        //{
        //    try
        //    {
        //        List<Category> categories = new List<Category>();
        //        ResponseOutcome<Category> categoryOutcome = await productsHttpService.ReadProductById();
        //        if (categoryOutcome.ResponseStatus == ResponseStatus.Success)
        //            categories = categoryOutcome.EntityList;

        //        return categories;
        //    }
        //    catch
        //    {
        //        throw
        //    }
        //}
        #endregion
    }
}
