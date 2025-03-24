using Microsoft.EntityFrameworkCore;
using VanTuongDuy_2280600501.Models;

namespace VanTuongDuy_2280600501.Reponsitories
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public EFProductRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                          .Include(p => p.Category) // Load thêm thông tin Category
                          .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            Console.WriteLine($"ID nhận được: {id}");
            if (id <= 0)
            {
                throw new ArgumentException("ID sản phẩm không hợp lệ.");
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy sản phẩm với ID: {id}");
            }
            return product;
        }
        



        public async Task AddAsync(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy sản phẩm với ID: {product.Id}");
            }

            _context.Entry(existingProduct).CurrentValues.SetValues(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy sản phẩm với ID: {id}");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public Task<string?> GetAllWithCategoryAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetAllWithCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public string? GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public string? GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product model)
        {
            throw new NotImplementedException();
        }

        public void AddProduct(Product model)
        {
            throw new NotImplementedException();
        }
    }
}
