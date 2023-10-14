using WebApp.Decorator.Models;

namespace WebApp.Decorator.Repositories.Decorator;

public class BaseProductRepositoryDecorator: IProductRepository
{
    private readonly IProductRepository _productRepository;

    public BaseProductRepositoryDecorator(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public virtual async Task<Product> GetByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public virtual async Task<List<Product>> GetAllAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public virtual async Task<List<Product>> GetAllByUserIdAsync(string userId)
    {
        return await _productRepository.GetAllByUserIdAsync(userId);
    }

    public virtual async Task<Product> AddAsync(Product product)
    {
        return await _productRepository.AddAsync(product);
    }

    public virtual async Task Remove(Product product)
    {
        await _productRepository.Remove(product);
    }

    public virtual async Task Update(Product product)
    {
        await _productRepository.Update(product);
    }
}