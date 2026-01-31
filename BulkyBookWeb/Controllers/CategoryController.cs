using BulkyBook.Application.Interfaces.Repositories;
using BulkyBook.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryRepository categoryRepository, ILogger<CategoryController> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _categoryRepository.GetAll();
            _logger.LogInformation("Loaded {Count} categories.", objCategoryList.Count());
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name.");
                _logger.LogWarning("Category validation failed: Name matches DisplayOrder.");
            }
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(obj);
                _categoryRepository.Save();
                _logger.LogInformation("Category created: {Name}.", obj.Name);
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");

            }
            _logger.LogWarning("Category creation failed due to validation errors.");
            return View(obj);
        }


        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 00)
            {
                _logger.LogWarning("Category edit requested with invalid id.");
                return NotFound();
            }
            var categoryFromDb = _categoryRepository.GetById(id.Value);

            if (categoryFromDb == null)
            {
                _logger.LogWarning("Category not found for edit. Id: {Id}.", id.Value);
                return NotFound();
            }

            return View(categoryFromDb);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name.");
                _logger.LogWarning("Category validation failed on edit: Name matches DisplayOrder. Id: {Id}.", obj.Id);
            }
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(obj);
                _categoryRepository.Save();
                _logger.LogInformation("Category updated. Id: {Id}.", obj.Id);
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");

            }
            _logger.LogWarning("Category update failed due to validation errors. Id: {Id}.", obj.Id);
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 00)
            {
                _logger.LogWarning("Category delete requested with invalid id.");
                return NotFound();
            }
            var categoryFromDb = _categoryRepository.GetById(id.Value);

            if (categoryFromDb == null)
            {
                _logger.LogWarning("Category not found for delete. Id: {Id}.", id.Value);
                return NotFound();
            }

            return View(categoryFromDb);
        }


        //POST
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 00)
            {
                _logger.LogWarning("Category delete POST requested with invalid id.");
                return NotFound();
            }

            var obj = _categoryRepository.GetById(id.Value);
            if(obj == null)
            {
                _logger.LogWarning("Category not found for delete POST. Id: {Id}.", id.Value);
                return NotFound();
            }

            _categoryRepository.Remove(obj);
            _categoryRepository.Save();
            _logger.LogInformation("Category deleted. Id: {Id}.", id.Value);
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
