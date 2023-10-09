using MongoDB.Driver;
using WebApp.Strategy.Models;

namespace WebApp.Strategy.Repositories;

public class ProductRepositoryFromMongoDb: IProductRepository
{
    private readonly IMongoCollection<Product> _mongoCollection;

    public ProductRepositoryFromMongoDb(IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("MongoDb");
        var client = new MongoClient(connString);
        var database = client.GetDatabase("UdemyDesignPatternDb");
        _mongoCollection = database.GetCollection<Product>("Products");
    }

    public async Task<Product> GetById(string id)
    {
        return await _mongoCollection.Find(x=>x.Id==id).FirstOrDefaultAsync();
    }

    public async Task<List<Product>> GetAllByUserId(string userId)
    {
        return await _mongoCollection.Find(x => x.UserId == userId).ToListAsync();
    }

    public async Task<Product> Save(Product product)
    {
        await _mongoCollection.InsertOneAsync(product);
        return product;
    }

    public async Task Delete(Product product)
    {
        await _mongoCollection.DeleteOneAsync(x => x.Id == product.Id);
    }

    public async Task Update(Product product)
    {
        await _mongoCollection.FindOneAndReplaceAsync(x => x.Id == product.Id, product);
    }
}