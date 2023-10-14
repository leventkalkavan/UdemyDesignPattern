using Microsoft.EntityFrameworkCore;
using WebApp.Decorator.Context;
using WebApp.Decorator.Models;

namespace WebApp.Decorator.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<List<Product>> GetAllByUserIdAsync(string userId)
    {
        return await _context.Products.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<Product> AddAsync(Product product)
    {
        await _context.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task Remove(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
}