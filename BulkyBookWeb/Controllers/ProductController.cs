using BulkyBook.Application.Interfaces.Repositories;
using BulkyBook.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objProductList = _productRepository.GetAll();
            _logger.LogInformation("Loaded {Count} products.", objProductList.Count());
            return View(objProductList);
        }


        //GET
        public IActionResult Create()
        {
            return View();
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product obj)
        {
            if (obj.Name == obj.Value.ToString())
            {
                ModelState.AddModelError("CustomError", "The Value cannot exactly match the Name.");
                _logger.LogWarning("Product validation failed: Name matches Value.");
            }
            if (ModelState.IsValid)
            {
                _productRepository.Add(obj);
                _productRepository.Save();
                _logger.LogInformation("Product created: {Name}.", obj.Name);
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");

            }
            _logger.LogWarning("Product creation failed due to validation errors.");
            return View(obj);
        }


        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 00)
            {
                _logger.LogWarning("Product edit requested with invalid id.");
                return NotFound();
            }
            var productFromDb = _productRepository.GetById(id.Value);

            if (productFromDb == null)
            {
                _logger.LogWarning("Product not found for edit. Id: {Id}.", id.Value);
                return NotFound();
            }

            return View(productFromDb);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product obj)
        {
            if (obj.Name == obj.Value.ToString())
            {
                ModelState.AddModelError("CustomError", "The Value cannot exactly match the Name.");
                _logger.LogWarning("Product validation failed on edit: Name matches Value. Id: {Id}.", obj.Id);
            }
            if (ModelState.IsValid)
            {
                _productRepository.Update(obj);
                _productRepository.Save();
                _logger.LogInformation("Product updated. Id: {Id}.", obj.Id);
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");

            }
            _logger.LogWarning("Product update failed due to validation errors. Id: {Id}.", obj.Id);
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 00)
            {
                _logger.LogWarning("Product delete requested with invalid id.");
                return NotFound();
            }
            var productFromDb = _productRepository.GetById(id.Value);

            if (productFromDb == null)
            {
                _logger.LogWarning("Product not found for delete. Id: {Id}.", id.Value);
                return NotFound();
            }

            return View(productFromDb);
        }


        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 00)
            {
                _logger.LogWarning("Product delete POST requested with invalid id.");
                return NotFound();
            }

            var obj = _productRepository.GetById(id.Value);
            if (obj == null)
            {
                _logger.LogWarning("Product not found for delete POST. Id: {Id}.", id.Value);
                return NotFound();
            }

            _productRepository.Remove(obj);
            _productRepository.Save();
            _logger.LogInformation("Product deleted. Id: {Id}.", id.Value);
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");

        }

    }
}
