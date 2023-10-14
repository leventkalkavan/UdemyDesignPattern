using Microsoft.Extensions.Caching.Memory;
using WebApp.Decorator.Models;

namespace WebApp.Decorator.Repositories.Decorator;

public class ProductRepositoryCacheDecorator: BaseProductRepositoryDecorator
{
    private readonly IMemoryCache _memoryCache;
    private const string ProductCacheName = "products";
    public ProductRepositoryCacheDecorator(IProductRepository productRepository, IMemoryCache memoryCache) : base(productRepository)
    {
        _memoryCache = memoryCache;
    }

    public async override Task<List<Product>> GetAllAsync()
    {
        if (_memoryCache.TryGetValue(ProductCacheName,out List<Product> cacheProducts))
        {
            return cacheProducts;
        }
        await UpdateCache();
        return _memoryCache.Get<List<Product>>(ProductCacheName);
    }

    public async override Task<List<Product>> GetAllByUserIdAsync(string userId)
    {
        var products = await GetAllAsync();
        return products.Where(x => x.UserId == userId).ToList();
    }

    public async override Task<Product> AddAsync(Product product)
    {
        await base.AddAsync(product);
        await UpdateCache();
        return product;
    }

    public async override Task Update(Product product)
    {
        await base.Update(product);
        await UpdateCache();
    }

    public async override Task Remove(Product product)
    {
        await base.Remove(product);
        await UpdateCache();
    }

    private async Task UpdateCache()
    {
        _memoryCache.Set(ProductCacheName, await base.GetAllAsync());
    }
}