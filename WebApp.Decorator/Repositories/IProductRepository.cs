using WebApp.Decorator.Models;

namespace WebApp.Decorator.Repositories;

public interface IProductRepository
{
    Task<Product> GetByIdAsync(int id);
    Task<List<Product>> GetAllAsync();
    Task<List<Product>> GetAllByUserIdAsync(string userId);
    Task<Product> AddAsync(Product product);
    Task Remove(Product product);
    Task Update(Product product);
}
