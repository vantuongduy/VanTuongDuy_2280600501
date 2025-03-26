using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VanTuongDuy_2280600501.Models;
using VanTuongDuy_2280600501.Reponsitories;

namespace VanTuongDuy_2280600501.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository productRepository,
       ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        //
        public async Task<IActionResult> Index  ()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }
        public async Task<IActionResult> AddAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            Console.WriteLine($"Categories count: {categories.Count()}"); // Kiểm tra số lượng danh mục
            foreach (var cat in categories)
            {
                Console.WriteLine($"Category ID: {cat.Id}, Name: {cat.Name}");
            }
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Display(int id)
        {
            Console.WriteLine($"ID nhận được: {id}");

            if (id <= 0) // Kiểm tra ID hợp lệ
            {
                return BadRequest("ID sản phẩm không hợp lệ.");
            }

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound("Không tìm thấy sản phẩm.");
            }
            return View(product);
        }
        //
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id)
        {
            Console.WriteLine($"ID nhận được: {id}");
            if (id <= 0)
            {
                return BadRequest("ID sản phẩm không hợp lệ.");
            }

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound("Không tìm thấy sản phẩm.");
            }
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name",
            product.CategoryId);
            return View(product);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            Console.WriteLine(id);
            if (id != product.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _productRepository.UpdateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product, IFormFile imageUrl)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null)
                {

                    product.ImageUrl = await SaveImage(imageUrl);
                }

               
                _productRepository.AddProduct(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            // Nếu thư mục không tồn tại, tạo mới
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, image.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return "/images/" + image.FileName; // Đường dẫn trả về để hiển thị ảnh

        }
    }
}
