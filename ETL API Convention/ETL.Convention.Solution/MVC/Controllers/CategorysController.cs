using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MVC.HttpServices;
using Newtonsoft.Json;
using Utilities.Constants;
using X.PagedList;

/*
 * Created by   : Jahangir
 * Date created : 27-03-2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  :
 * Date reviewed:
 */
namespace MVC.Controllers
{
    public class CategorysController : Controller
    {
        private readonly CategorysHttpService categorysHttpService;
        private readonly IHttpContextAccessor httpContextAccessor;

        private ISession? session => httpContextAccessor.HttpContext?.Session;

        public CategorysController(CategorysHttpService categorysHttpService, IHttpContextAccessor httpContextAccessor)
        {
            this.categorysHttpService = categorysHttpService;
            this.httpContextAccessor = httpContextAccessor;
        }
        #region Index
        //public async Task<IActionResult> Index()
        //{
        //    try
        //    {
        //        var outcome = await categorysHttpService.ReadCategorys();

        //        if (outcome.ResponseStatus == ResponseStatus.Success)
        //        {
        //            return View(outcome.EntityList);
        //        }
        //        else
        //        {
        //            return View("Error");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        public async Task<IActionResult> Index()
        {
            ResponseOutcome<Category> categorysOutcome = await categorysHttpService.ReadCategorys();

            if (categorysOutcome.ResponseStatus == ResponseStatus.Success)
                return View(categorysOutcome.EntityList);

            return View(categorysOutcome);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            ResponseOutcome<Category> categoryOutcome = await categorysHttpService.CreateCategory(category);

            if (categoryOutcome.ResponseStatus == ResponseStatus.Failed)
            {
                TempData[SessionConstants.Message] = categoryOutcome.Message;
            }

            if (categoryOutcome.ResponseStatus == ResponseStatus.Success)
            {
                TempData[SessionConstants.Message] = MessageConstants.CategoryCreatedSuccessfully;

                return RedirectToAction("Index", new { id = categoryOutcome.Entity.CategoryId });
            }

            return View();
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int id)
        {
            ResponseOutcome<Category> categoryOutcome = await categorysHttpService.ReadCategoryById(id);
            if (categoryOutcome.ResponseStatus == ResponseStatus.Success)
                return View(categoryOutcome.Entity);
            else
                return NotFound();
        }
        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int id)
        {
            ResponseOutcome<Category> categoryOutcome = await categorysHttpService.ReadCategoryById(id);
            if (categoryOutcome.ResponseStatus == ResponseStatus.Success)
                return View(categoryOutcome.Entity);
            else
                return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            ResponseOutcome<Category> categoryOutcome = await categorysHttpService.UpdateCategory(category);

            if (categoryOutcome.ResponseStatus == ResponseStatus.Failed)
            {
                TempData[SessionConstants.Message] = categoryOutcome.Message;
            }

            if (categoryOutcome.ResponseStatus == ResponseStatus.Success)
            {
                TempData[SessionConstants.Message] = MessageConstants.CategoryUpdatedSuccessfully;

                return RedirectToAction("Details", new { id = categoryOutcome.Entity.CategoryId });
            }

            return View();
        }
        #endregion

        
    }
}
