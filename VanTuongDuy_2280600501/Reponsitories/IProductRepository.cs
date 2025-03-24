using VanTuongDuy_2280600501.Models;

namespace VanTuongDuy_2280600501.Reponsitories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<string?> GetAllWithCategoryAsync();
        Task<string?> GetAllWithCategoriesAsync();
        string? GetAllProducts();
        void DeleteProduct(int id);
        string? GetProductById(int id);
        void UpdateProduct(Product model);
        void AddProduct(Product model);
    }
}
