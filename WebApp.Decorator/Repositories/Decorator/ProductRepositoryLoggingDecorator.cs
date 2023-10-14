using WebApp.Decorator.Models;

namespace WebApp.Decorator.Repositories.Decorator;

public class ProductRepositoryLoggingDecorator: BaseProductRepositoryDecorator
{
    private readonly ILogger<ProductRepositoryLoggingDecorator> _logger;
    public ProductRepositoryLoggingDecorator(IProductRepository productRepository, ILogger<ProductRepositoryLoggingDecorator> logger) : base(productRepository)
    {
        _logger = logger;
    }

    public override Task<List<Product>> GetAllAsync()
    {
        _logger.LogInformation("getall called");
        return base.GetAllAsync();
    }

    public override Task<List<Product>> GetAllByUserIdAsync(string userId)
    {
        _logger.LogInformation("GetAllByUserId called");
        return base.GetAllByUserIdAsync(userId);
    }

    public override Task<Product> GetByIdAsync(int id)
    {
        _logger.LogInformation("GetById called");
        return base.GetByIdAsync(id);
    }

    public override Task<Product> AddAsync(Product product)
    {
        _logger.LogInformation("Add called");
        return base.AddAsync(product);
    }

    public override Task Update(Product product)
    {
        _logger.LogInformation("Update called");
        return base.Update(product);
    }

    public override Task Remove(Product product)
    {
        _logger.LogInformation("Remove called");
        return base.Remove(product);
    }
}